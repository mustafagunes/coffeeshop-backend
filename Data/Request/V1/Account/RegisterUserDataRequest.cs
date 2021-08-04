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
        // Definitions
        private readonly IUserRepository _repository;

        public RegisterUserDataRequestHandler(IUserRepository repository)
        {
            _repository = repository;
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
            
            await _repository.AddAsync(userModel);

            var user = await _repository.GetWithEmailAsync(userModel.Email, cancellationToken);

            return user;
        }
    }
}