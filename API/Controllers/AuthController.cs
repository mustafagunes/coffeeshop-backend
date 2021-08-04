using System.Threading.Tasks;
using CoffeeShop.API.Models;
using CoffeeShop.API.Models.Account;
using CoffeeShop.API.Models.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Service.Request.V1.Auth;

namespace CoffeeShop.API.Controllers
{
    
    /*
     * Auth akışı özet
     * Kullanıcı sisteme girer register olur. Register olduğunda kullanıcıya 5 dakikalık AccessToken üretilir. Kullanıcı login olurken
     * refreshToken tablosuna userId, refreshToken ve refreshTokenın expire süresi yazdırılır. Bunun haricinde AccessToken expire
     * olduğunda Clienta 401 döner Cliet bu durumda kullanıcıyı logine göndermeden önce refreshToken apisine istek atar.
     * Refresh token apisi Cliet tarafından gönderilen refreshTokenı refreshToken tablosunda arar ve kullanıcıyı bulursa ve refreshTokenın
     * expire time'ı dolmamışsa kullanıcıya yeni bir accessToken üretir ve Client'a döner. Şayet kullanıcı sisteme register oldu, bu süre
     * içerisinde accessToken ve refreshToken expire oldu ise Client elindeki accessToken ile istek atar 401 döner ve refreshToken apiside
     * tokenın expire olduğunu bilidir. Bu durumda kullanıcı login flowuna aktarılır. Burada oluşan kafa karşıklığı kullanıcı tekrar login
     * olduğunda refreshToken tablosuna yeniden bir kayıt atmak gerekmesidir ve bu döngüde yüz milyonlarca kayıt atılmasıdır. Bunun içinde
     * yapılması gereken database tarafında bir job tanımlayıp expire tarihi dolmuş verileri her ay veya her gün (sizin ihtiyacınıza kalmış)
     * gece saatlerinde bu verilerin databaseden silinmesidir.
     * Şayet kullanıcı sistemden logout olursa bu durumda o kullanıcının idsi ile refreshToken tablosundaki tüm tokenlar silinir. Böylece
     * accessToken'a verdiğimiz 5 dakika sonrasında kullanıcı mecburen login olması gerekecektir.
     */
    
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