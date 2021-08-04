using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Service.V1.Auth
{
    public class LogoutRequest : IRequest<LogoutRequestResponse>
    {
        public LogoutRequest(int userId)
        {
            UserId = userId;
        }

        public int UserId { get; }
    }

    public class LogoutRequestHandler : IRequestHandler<LogoutRequest, LogoutRequestResponse>
    {
        private readonly IMediator _mediator;

        public LogoutRequestHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<LogoutRequestResponse> Handle(LogoutRequest request, CancellationToken cancellationToken)
        {
            // delete all refresh token by user id
            
            var response = new LogoutRequestResponse
            {
                IsSuccess = true
            };

            return response;
        }
    }

    public class LogoutRequestResponse
    {
        public bool IsSuccess { get; set; }
    }
}