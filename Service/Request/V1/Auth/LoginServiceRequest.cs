using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Model.Base;
using Core.Security;
using Data.Request.V1.Auth;
using MediatR;

namespace Service.Request.V1.Auth
{
    public class LoginServiceRequest : IRequest<BaseObjectResponse>
    {
        public LoginServiceRequest(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; }
        public string Password { get; }
    }

    public class LoginServiceRequestHandler : IRequestHandler<LoginServiceRequest, BaseObjectResponse>
    {
        private readonly IMediator _mediator;
        private readonly IJwtHelper _jwtHelper;

        public LoginServiceRequestHandler(IMediator mediator, IJwtHelper jwtHelper)
        {
            _mediator = mediator;
            _jwtHelper = jwtHelper;
        }

        public async Task<BaseObjectResponse> Handle(LoginServiceRequest request, CancellationToken cancellationToken)
        {
            var loginDataRequestResponse = await _mediator.Send(new 
                    LoginDataRequest(
                        request.Email,
                        request.Password
                    ), cancellationToken
            );

            if (loginDataRequestResponse == null)
                return new BaseObjectResponse(false, "Kullanıcı bulunamadı", null);

            var response = new BaseObjectResponse(true, "Giriş Başarılı!", loginDataRequestResponse);

            return response;
        }
    }
}