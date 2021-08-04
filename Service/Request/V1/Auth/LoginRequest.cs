using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Service.Request.V1.Auth
{
    public class LoginRequest : IRequest<LoginRequestResponse>
    {
        public LoginRequest(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; }
        public string Password { get; }
    }

    public class LoginRequestHandler : IRequestHandler<LoginRequest, LoginRequestResponse>
    {
        private readonly IMediator _mediator;

        public LoginRequestHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<LoginRequestResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            // var user = get user by username and password

            // if user not exist return exception

            var refreshToken = Guid.NewGuid();

            var dateEnd = DateTime.Now.AddDays(30);

            // insert refreshToken to db (user.id, refreshToken, dateEnd)

            var accessToken = string.Empty; // jwt helper, duration short 5 min

            var response = new LoginRequestResponse
            {
                AccessToken = accessToken, // 5 min
                RefreshToken = refreshToken // 30 day
            };

            return response;
        }
    }

    public class LoginRequestResponse
    {
        public string AccessToken { get; set; }
        public Guid RefreshToken { get; set; }
    }
}