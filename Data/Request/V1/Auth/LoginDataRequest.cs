using System;
using System.Collections.Generic;
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
    public class LoginDataRequest : IRequest<LoginDataRequestResponse>
    {
        public LoginDataRequest(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; }
        public string Password { get; }
    }

    public class LoginDataRequestHandler : IRequestHandler<LoginDataRequest, LoginDataRequestResponse>
    {
        private readonly IJwtHelper _jwtHelper;
        private readonly IUserRepository _userRepository;
        private readonly IRepository<RefreshToken> _refreshTokenRepository;
        
        public LoginDataRequestHandler(IUserRepository userRepository, IRepository<RefreshToken> refreshTokenRepository, IJwtHelper jwtHelper)
        {
            _jwtHelper = jwtHelper;
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<LoginDataRequestResponse> Handle(LoginDataRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Login(request.Email, request.Password, cancellationToken);
            
            if (user == null) 
                return null;

            var refreshTokenModel = new RefreshToken
            {
                UserId = user.Id,
                Token = Guid.NewGuid().ToString(),
                DateEnd = DateTime.Now.AddDays(30)
            };

            var tokens = await _refreshTokenRepository.Where(t => t.UserId == user.Id);
            _refreshTokenRepository.RemoveRange(tokens);
            await _refreshTokenRepository.AddAsync(refreshTokenModel);
            
            var accessToken = _jwtHelper.CreateToken(user: user);
            var refreshToken = refreshTokenModel.Token;

            var loginResponse = new LoginDataRequestResponse
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };

            return loginResponse;
        }
    }
    
    public class LoginDataRequestResponse
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}