using System.Threading;
using System.Threading.Tasks;
using Core.Model.Base;
using Data.Request.V1.Account;
using Data.Request.V1.User;
using MediatR;

namespace Service.Request.V1.User
{
    public class RegisterNotificationServiceRequest : IRequest<BaseObjectResponse>
    {
        public RegisterNotificationServiceRequest(int userId, string fcmToken, string apnsToken)
        {
            UserId = userId;
            FcmToken = fcmToken;
            ApnsToken = apnsToken;
        }

        public int UserId { get; }
        public string FcmToken { get; }
        public string ApnsToken { get; }
    }

    public class RegisterNotificationServiceRequestHandler : IRequestHandler<RegisterNotificationServiceRequest, BaseObjectResponse>
    {
        // Definitions
        private readonly IMediator _mediator;

        public RegisterNotificationServiceRequestHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<BaseObjectResponse> Handle(RegisterNotificationServiceRequest request, CancellationToken cancellationToken)
        {
            var registerNotificationResponse = await _mediator.Send(
                new RegisterNotificationDataRequest(
                    request.UserId,
                    request.FcmToken,
                    request.ApnsToken
                ), cancellationToken
            );

            var successResponse = new BaseObjectResponse(true, "Token başarı ile kaydedildi.", registerNotificationResponse);
            var failureResponse = new BaseObjectResponse(false, "Token kaydedilirken bir sorun oluştu.", registerNotificationResponse);

            if (registerNotificationResponse == null)
                return failureResponse;

            return registerNotificationResponse.IsSuccess
                ? successResponse
                : failureResponse;
        }
    }
}