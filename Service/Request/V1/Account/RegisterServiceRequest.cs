using System.Threading;
using System.Threading.Tasks;
using Core.Security;
using Data.Request.V1.Account;
using MediatR;

namespace Service.Request.V1.Account
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

        public RegisterServiceRequestHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<RegisterServiceRequestResponse> Handle(RegisterServiceRequest request, CancellationToken cancellationToken)
        {
            var user = await _mediator.Send(new 
                RegisterUserDataRequest(
                    request.Email,
                    request.FullName,
                    request.Password,
                    request.ApnsToken,
                    request.FcmToken
                ), cancellationToken
            );
            
            var response = new RegisterServiceRequestResponse
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
            };

            return response;
        }
    }

    public class RegisterServiceRequestResponse
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    }
}