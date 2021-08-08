using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Core.Interface;
using Core.Model;
using Core.Model.Base;
using Core.Security;
using MediatR;

namespace Data.Request.V1.Auth
{
    public class RefreshTokenDataRequest : IRequest<RefreshTokenDataRequestResponse>
    {
        public RefreshTokenDataRequest(string refreshToken)
        {
            RefreshToken = refreshToken;
        }

        public string RefreshToken { get; }
    }

    public class RefreshTokenDataRequestHandler : IRequestHandler<RefreshTokenDataRequest, RefreshTokenDataRequestResponse>
    {
        private readonly IJwtHelper _jwtHelper;
        private readonly IUserRepository _userRepository;
        private readonly IRepository<RefreshToken> _refreshTokenRepository;
        
        public RefreshTokenDataRequestHandler(IJwtHelper jwtHelper, IUserRepository userRepository, IRepository<RefreshToken> refreshTokenRepository)
        {
            _jwtHelper = jwtHelper;
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<RefreshTokenDataRequestResponse> Handle(RefreshTokenDataRequest request, CancellationToken cancellationToken)
        {
            var tokenResult = await _refreshTokenRepository.SingleOrDefaultAsync(t => t.Token == request.RefreshToken);

            if (tokenResult == null)
                return null;

            if (tokenResult.IsExpired())
                return null;

            var user = await _userRepository.SingleOrDefaultAsync(u => u.Id == tokenResult.UserId);

            if (user == null)
                return null;

            // Remove legacy refreshToken
            var tokens = await _refreshTokenRepository.Where(t => t.UserId == user.Id);
            _refreshTokenRepository.RemoveRange(tokens);
            
            // Add new refreshToken
            var refreshTokenModel = new RefreshToken
            {
                UserId = user.Id,
                Token = Guid.NewGuid().ToString(),
                DateEnd = DateTime.Now.AddDays(30)
            };
            await _refreshTokenRepository.AddAsync(refreshTokenModel);
            
            // Create response
            var accessToken = _jwtHelper.CreateToken(user: user);
            var refreshToken = refreshTokenModel.Token;

            var response = new RefreshTokenDataRequestResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                DateEnd = refreshTokenModel.DateEnd
            };

            return response;
        }
    }
    
    public class RefreshTokenDataRequestResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime DateEnd { get; set; }
    }
}