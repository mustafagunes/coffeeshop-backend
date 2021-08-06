using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Core.Interface;
using Core.Model;
using Core.Security;
using MediatR;

namespace Data.Request.V1.Auth
{
    public class LoginDataRequest : IRequest<LoginRequestResponse>
    {
        public LoginDataRequest(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; }
        public string Password { get; }
    }

    public class LoginDataRequestHandler : IRequestHandler<LoginDataRequest, LoginRequestResponse>
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

        public async Task<LoginRequestResponse> Handle(LoginDataRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Login(request.Email, request.Password, cancellationToken);
            
            if (user == null)
            {
                return null;
            }

            var refreshTokenModel = new RefreshToken
            {
                UserId = user!.Id,
                Token = Guid.NewGuid().ToString(),
                DateEnd = DateTime.Now.AddDays(30)
            };
            
            await _refreshTokenRepository.AddAsync(refreshTokenModel);

            var claims = new List<Claim>() {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Role,user.RoleId.ToString()),
            };

            var accessToken = _jwtHelper.CreateToken(claims);
            var refreshToken = refreshTokenModel.Token;

            var response = new LoginRequestResponse
            {
                UserId = user.Id,
                FullName = user.FullName,
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };

            return response;
        }
    }
    
    public class LoginRequestResponse
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}