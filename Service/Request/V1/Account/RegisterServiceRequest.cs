using System.Threading;
using System.Threading.Tasks;
using Core.Model.Base;
using Core.Security;
using Data.Request.V1.Account;
using MediatR;

namespace Service.Request.V1.Account
{
    public class RegisterServiceRequest : IRequest<BaseObjectResponse>
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

    public class RegisterServiceRequestHandler : IRequestHandler<RegisterServiceRequest, BaseObjectResponse>
    {
        // Definitions
        private readonly IMediator _mediator;

        public RegisterServiceRequestHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<BaseObjectResponse> Handle(RegisterServiceRequest request, CancellationToken cancellationToken)
        {
            var registerDataRequestResponse = await _mediator.Send(new 
                RegisterUserDataRequest(
                    request.Email,
                    request.FullName,
                    request.Password,
                    request.ApnsToken,
                    request.FcmToken
                ), cancellationToken
            );

            if (registerDataRequestResponse == null)
                return new BaseObjectResponse(false, "Kayıt esnasında bir sorun oluştu. Lütfen tekrar deneyiniz!", null);

            var response = new BaseObjectResponse(true, "Kayıt Başarılı!", registerDataRequestResponse);

            return response;
        }
    }
}