using System.Threading;
using System.Threading.Tasks;
using Core.Model;
using Core.Model.Base;
using Core.Security;
using Data.Request.UserRequest;
using MediatR;

namespace Service.Account
{
    public class RegisterServiceRequest : IRequest<BaseObjectResponse<User>>
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string ApnsToken { get; set; }
        public string FcmToken { get; set; }
    }
    
    public class RegisterServiceRequestHandler : IRequestHandler<RegisterServiceRequest, BaseObjectResponse<User>>
    {
        // Definitions
        private readonly IMediator _mediator;
        private readonly IJwtHelper _jwtHelper;
        
        public RegisterServiceRequestHandler(IMediator mediator, IJwtHelper jwtHelper)
        {
            _mediator = mediator;
            _jwtHelper = jwtHelper;
        }

        public async Task<BaseObjectResponse<User>> Handle(RegisterServiceRequest request, CancellationToken cancellationToken)
        {
            var user = await _mediator.Send(new RegisterUserDataRequest()
            {
                FullName = request.FullName,
                Email = request.Email,
                ApnsToken = request.ApnsToken,
                FcmToken = request.FcmToken,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
            }, cancellationToken);
            
            var response = new BaseObjectResponse<User>()
            {
                Status = true,
                Message = "Success",
                Data = user
            };

            return response;
        }
    }
}