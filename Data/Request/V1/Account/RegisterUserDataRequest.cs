using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Interface;
using Core.Model;
using MediatR;

namespace Data.Request.V1.Account
{
    public class RegisterUserDataRequest : IRequest<User>
    {
        public RegisterUserDataRequest(string email, string fullName, string password, string apnsToken, string fcmToken)
        {
            Email = email;
            FullName = fullName;
            Password = password;
            ApnsToken = apnsToken;
            FcmToken = fcmToken;
        }

        public string Email { get; }
        public string FullName { get; }
        public string Password { get; }
        public string ApnsToken { get; }
        public string FcmToken { get; }
    }

    public class RegisterUserDataRequestHandler : IRequestHandler<RegisterUserDataRequest, User>
    {
        private readonly IUserRepository _userRepository;
        private readonly IRepository<RefreshToken> _refreshTokenRepository;

        public RegisterUserDataRequestHandler(IUserRepository userRepository, IRepository<RefreshToken> refreshTokenRepository)
        {
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<User> Handle(RegisterUserDataRequest request, CancellationToken cancellationToken)
        {
            var userModel = new User
            {
                Email = request.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                FullName = request.FullName,
                ApnsToken = request.ApnsToken,
                FcmToken = request.FcmToken
            };

            await _userRepository.AddAsync(userModel);
            
            var user = await _userRepository.GetWithEmailAsync(userModel.Email, cancellationToken);

            var refreshToken = new RefreshToken
            {
                UserId = user.Id,
                Token = Guid.NewGuid().ToString(),
                DateEnd = DateTime.Now.AddDays(30)
            };

            await _refreshTokenRepository.AddAsync(refreshToken);

            return user;
        }
    }
}