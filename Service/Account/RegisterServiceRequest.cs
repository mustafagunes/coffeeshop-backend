using System.Threading;
using System.Threading.Tasks;
using Core.Model;
using Core.Model.Base;
using Core.Security;
using Data.Request.UserRequest;
using MediatR;

namespace Service.Account
{
    public class RegisterServiceRequest : IRequest<RegisterServiceRequestResponse>
    {
        public RegisterServiceRequest(string email, string fullName, string password, string apnsToken, string fcmToken)
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

    public class RegisterServiceRequestHandler : IRequestHandler<RegisterServiceRequest, RegisterServiceRequestResponse>
    {
        // Definitions
        private readonly IMediator _mediator;
        private readonly IJwtHelper _jwtHelper;

        public RegisterServiceRequestHandler(IMediator mediator, IJwtHelper jwtHelper)
        {
            _mediator = mediator;
            _jwtHelper = jwtHelper;
        }

        public async Task<RegisterServiceRequestResponse> Handle(RegisterServiceRequest request, CancellationToken cancellationToken)
        {
            var hashPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var user = await _mediator.Send(new RegisterUserDataRequest(request.Email, request.FullName, hashPassword, request.ApnsToken, request.FcmToken), cancellationToken);
            
            var response = new RegisterServiceRequestResponse
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                RefreshToken = user.RefreshToken
            };

            return response;
        }
    }

    public class RegisterServiceRequestResponse
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string RefreshToken { get; set; }
    }
}