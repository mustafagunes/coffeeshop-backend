using System.Threading;
using System.Threading.Tasks;
using Core.Interface;
using Core.Model;
using Core.Model.Base;
using Data.Request.V1.Auth;
using MediatR;

namespace Service.Request.V1.Auth
{
    public class LogoutServiceRequest : IRequest<BaseObjectResponse>
    {
        public LogoutServiceRequest(int userId)
        {
            UserId = userId;
        }

        public int UserId { get; }
    }

    public class LogoutServiceRequestHandler : IRequestHandler<LogoutServiceRequest, BaseObjectResponse>
    {
        private readonly IMediator _mediator;

        public LogoutServiceRequestHandler(IMediator mediator, IRepository<RefreshToken> refreshTokenRepository)
        {
            _mediator = mediator;
        }

        public async Task<BaseObjectResponse> Handle(LogoutServiceRequest request, CancellationToken cancellationToken)
        {
            // delete all refresh token by user id
            var logoutDataRequestResponse = await _mediator.Send(
                new LogoutDataRequest(
                    request.UserId
                ), cancellationToken
            );

            if (logoutDataRequestResponse == null)
                return new BaseObjectResponse(false, "Silme işlemi başarısız.", null);

            var response = new BaseObjectResponse(true, "RefreshToken silme işlemi başarılı!", null);

            return response;
        }
    }
}