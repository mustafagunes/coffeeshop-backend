using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Interface;
using Core.Model;
using MediatR;

namespace Data.Request.V1.Account
{
    public class RegisterUserDataRequest : IRequest<RegisterDataRequestResponse>
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

    public class RegisterUserDataRequestHandler : IRequestHandler<RegisterUserDataRequest, RegisterDataRequestResponse>
    {
        private readonly IUserRepository _userRepository;

        public RegisterUserDataRequestHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<RegisterDataRequestResponse> Handle(RegisterUserDataRequest request, CancellationToken cancellationToken)
        {
            var userModel = new User
            {
                Email = request.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                RoleId = "1",
                FullName = request.FullName,
                ApnsToken = request.ApnsToken,
                FcmToken = request.FcmToken
            };

            var result = await _userRepository.AddAsync(userModel);

            if (result == false)
                return null;
            
            var user = await _userRepository.GetWithEmailAsync(userModel.Email, cancellationToken);

            if (user == null)
                return null;

            var response = new RegisterDataRequestResponse
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email
            };

            return response;
        }
    }
    public class RegisterDataRequestResponse
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    }
}