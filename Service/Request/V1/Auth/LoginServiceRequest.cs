using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Security;
using Data.Request.V1.Auth;
using MediatR;

namespace Service.Request.V1.Auth
{
    public class LoginServiceRequest : IRequest<LoginRequestResponse>
    {
        public LoginServiceRequest(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; }
        public string Password { get; }
    }

    public class LoginServiceRequestHandler : IRequestHandler<LoginServiceRequest, LoginRequestResponse>
    {
        private readonly IMediator _mediator;
        private readonly IJwtHelper _jwtHelper;

        public LoginServiceRequestHandler(IMediator mediator, IJwtHelper jwtHelper)
        {
            _mediator = mediator;
            _jwtHelper = jwtHelper;
        }

        public async Task<LoginRequestResponse> Handle(LoginServiceRequest request, CancellationToken cancellationToken)
        {
            var loginRequestResponse = await _mediator.Send(new 
                    LoginDataRequest(
                        request.Email,
                        request.Password
                    ), cancellationToken
            );

            // var user = get user by username and password

            // if user not exist return exception

            var response = new LoginRequestResponse
            {
                UserId = loginRequestResponse.UserId,
                FullName = loginRequestResponse.FullName,
                AccessToken = loginRequestResponse.AccessToken, // 5 min
                RefreshToken = loginRequestResponse.RefreshToken // 30 day
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