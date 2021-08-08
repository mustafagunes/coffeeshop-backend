using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Model;
using Core.Model.Base;
using Data.Request.V1.Auth;
using MediatR;

namespace Service.Request.V1.Auth
{
    public class RefreshTokenRequest : IRequest<BaseObjectResponse>
    {
        public RefreshTokenRequest(string refreshToken)
        {
            RefreshToken = refreshToken;
        }

        public string RefreshToken { get; }
    }

    public class RefreshTokenRequestHandler : IRequestHandler<RefreshTokenRequest, BaseObjectResponse>
    {
        private readonly IMediator _mediator;

        public RefreshTokenRequestHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<BaseObjectResponse> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            // var refreshToken = get refresh token by id

            // if refreshToken not exist return exception

            // if refreshToken endDate < datetimeNow return exception (data request)

            // refreshtoken.UserId

            var refreshTokenDataRequestResponse = await _mediator.Send(
                new RefreshTokenDataRequest(request.RefreshToken), cancellationToken
            );

            if (refreshTokenDataRequestResponse == null)
                return new BaseObjectResponse(false, "RefresToken ile ilgili bir sorun oluştu.", null);

            var response = new BaseObjectResponse(true, "Yeni refreshToken oluşturuldu.", refreshTokenDataRequestResponse);
                    
            return response;
        }
    }
}