using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Service.Account;

namespace CoffeeShop.API.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        
        // Definitions
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterServiceRequest registerServiceRequest)
        {
            var user = await _mediator.Send(registerServiceRequest);
            return Ok(user);
        }
        
        # region legacy code

        // // Definitions
        // private readonly UserManager<AppUser> _userManager;
        // private readonly SignInManager<AppUser> _signInManager;
        // private readonly ITokenService _tokenService;
        //
        // public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService)
        // {
        //     _userManager = userManager;
        //     _signInManager = signInManager;
        //     _tokenService = tokenService;
        // }
        //
        // [Authorize(Roles = Role.Admin + "," + Role.User)]
        // [HttpGet]
        // public async Task<ActionResult<UserDto>> GetCurrentUser()
        // {
        //     var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
        //     var platform = Request.GetHeader("Platform");
        //     
        //     var userResponse = new UserDto
        //     {
        //         Id = user.Id,
        //         Email = user.Email,
        //         Token = await _tokenService.CreateToken(user),
        //         FullName = user.FullName,
        //         Platform = platform
        //     };
        //     
        //     var response = new BaseObjectResponse<UserDto>()
        //     {
        //         Status = true,
        //         Message = "Success",
        //         Data = userResponse
        //     };
        //
        //     return Ok(response);
        // }
        //
        // [HttpPost("login")]
        // public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        // {
        //     BaseObjectResponse<UserDto> response;
        //     
        //     var user = await _userManager.FindByEmailAsync(loginDto.Email);
        //
        //     if (user == null)
        //     {
        //         response = new BaseObjectResponse<UserDto>()
        //         {
        //             Status = false,
        //             Message = "User not found",
        //             Data = null
        //         };
        //         return Unauthorized(response);
        //     }
        //
        //     var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, true);
        //
        //     if (result.IsLockedOut)
        //     {
        //         response = new BaseObjectResponse<UserDto>()
        //         {
        //             Status = false,
        //             Message = "You are locked out! Please try again or reset password",
        //             Data = null
        //         };
        //         return Unauthorized(response);
        //     }
        //
        //     if (!result.Succeeded)
        //     {
        //         response = new BaseObjectResponse<UserDto>()
        //         {
        //             Status = false,
        //             Message = "Unknown",
        //             Data = null
        //         };
        //         return Unauthorized(response);
        //     }
        //
        //     var token = await _tokenService.CreateToken(user);
        //     
        //     var userDto = new UserDto
        //     {
        //         Id = user.Id,
        //         Email = user.Email,
        //         Token = token,
        //         FullName = user.FullName
        //     };
        //
        //     response = new BaseObjectResponse<UserDto>()
        //     {
        //         Status = true,
        //         Message = "Login Success",
        //         Data = userDto
        //     };
        //
        //     return Ok(response);
        // }
        //
        // [HttpPost("register")]
        // public async Task<ActionResult<UserDto>> RegisterWithConfirmEmail(RegisterDto registerDto)
        // {
        //     BaseObjectResponse<UserDto> response;
        //     
        //     if (CheckEmailExistsAsync(registerDto.Email).Result.Value)
        //     {
        //         response = new BaseObjectResponse<UserDto>()
        //         {
        //             Status = false,
        //             Message = "Email address is in use",
        //             Data = null
        //         };
        //         return BadRequest(response);
        //     }
        //     
        //     var user = new AppUser
        //     {
        //         FullName = registerDto.FullName,
        //         Email = registerDto.Email,
        //         UserName = registerDto.Email,
        //         ApnsToken = registerDto.ApnsToken,
        //         FcmToken = registerDto.FcmToken
        //     };
        //
        //     var result = await _userManager.CreateAsync(user, registerDto.Password);
        //     await _userManager.AddToRoleAsync(user, Role.User);
        //
        //     if (!result.Succeeded)
        //     {
        //         response = new BaseObjectResponse<UserDto>()
        //         {
        //             Status = false,
        //             Message = result.Errors.First().Description,
        //             Data = null
        //         };
        //         return BadRequest(response);
        //     }
        //     
        //     var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        //     await _userManager.ConfirmEmailAsync(user, token);
        //
        //     var userDto = new UserDto()
        //     {
        //         FullName = user.FullName,
        //         Token = token, // Instead of jwt, it is going to be email confirm token
        //         Email = user.Email,
        //         Id = user.Id
        //     };
        //
        //     response = new BaseObjectResponse<UserDto>()
        //     {
        //         Status = true,
        //         Message = "User created",
        //         Data = userDto
        //     };
        //
        //     return Ok(response);
        // }
        //
        // [HttpPost]
        // [Route("registerNotificationToken")]
        // [Authorize(Roles = Role.Admin + "," + Role.User)]
        // public async Task<ActionResult> RegisterNotificationToken(NotificationTokenDto notificationTokenDto)
        // {
        //     var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
        //     
        //     user.ApnsToken = notificationTokenDto.ApnsToken;
        //     user.FcmToken = notificationTokenDto.FcmToken;
        //     
        //     var result = await _userManager.UpdateAsync(user);
        //     
        //     if (result.Succeeded)
        //     {
        //         return Ok();
        //     }
        //
        //     return BadRequest();
        // }
        //
        // // Helper Functions
        // private async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
        // {
        //     return await _userManager.FindByEmailAsync(email) != null;
        // }

        #endregion
    }
}