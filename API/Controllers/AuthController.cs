using System.Threading.Tasks;
using CoffeeShop.API.Models;
using CoffeeShop.API.Models.Account;
using CoffeeShop.API.Models.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Service.Account;
using Service.V1.Auth;

namespace CoffeeShop.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel model)
        {
            var token = await _mediator.Send(new LoginRequest(model.Email, model.Password));

            return Ok(token);
        }
        
        // when any request returns 401 try to refresh token
        // if refresh token returns 401 logout
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenModel model)
        {
            var token = await _mediator.Send(new RefreshTokenRequest(model.RefreshToken));

            return Ok(token);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            // get user id from token
            
            var token = await _mediator.Send(new LogoutRequest(1));

            return Ok(token);
        }
        
        
    }
}