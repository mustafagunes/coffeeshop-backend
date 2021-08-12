using System.Threading.Tasks;
using CoffeeShop.API.Models.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Service.Request.V1.Account;
using Service.Request.V1.User;

namespace CoffeeShop.API.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register-notification")]
        public async Task<IActionResult> Register([FromBody] RegisterNotificationRequestModel model)
        {
            var response = await _mediator.Send(new 
                RegisterNotificationServiceRequest(
                    model.UserId,
                    model.FcmToken,
                    model.ApnsToken
                )
            );
            
            return response.Status ? (IActionResult) Ok(response) : BadRequest(response);
        }
    }
}