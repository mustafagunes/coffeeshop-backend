using System.Threading;
using System.Threading.Tasks;
using Core.Interface;
using Core.Model;
using Data.Context;
using MediatR;

namespace Data.Request.UserRequest
{
    public class RegisterUserDataRequest : IRequest<User>
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string ApnsToken { get; set; }
        public string FcmToken { get; set; }
    }
    
    // public class RegisterDataResponse
    // {
    //     public string Email { get; set; }
    //     public string FullName { get; set; }
    //     public string Token { get; set; }
    //     public string Id { get; set; }
    // }

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

            var user = await _repository.GetWithEmailAsync(userModel.Email);

            return user;
        }
    }
}