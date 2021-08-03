using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Service.V1.Auth
{
    public class RefreshTokenRequest : IRequest<RefreshTokenRequestResponse>
    {
        public RefreshTokenRequest(Guid refreshToken)
        {
            RefreshToken = refreshToken;
        }

        public Guid RefreshToken { get; }
    }

    public class RefreshTokenRequestHandler : IRequestHandler<RefreshTokenRequest, RefreshTokenRequestResponse>
    {
        private readonly IMediator _mediator;

        public RefreshTokenRequestHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<RefreshTokenRequestResponse> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            // var refreshToken = get refresh token by id

            // if refreshToken not exist return exception

            // if refreshToken endDate < datetimeNow return exception (data request)

            // refreshtoken.UserId

            var accessToken = string.Empty; // jwt helper, duration short 5 min

            var response = new RefreshTokenRequestResponse
            {
                AccessToken = accessToken, // 5 min
            };

            return response;
        }
    }

    public class RefreshTokenRequestResponse
    {
        public string AccessToken { get; set; }
    }
}