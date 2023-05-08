using Microsoft.AspNetCore.Mvc;
using TheFarmingGame.Domains;
using TheFarmingGame.Domains.Requests;
using TheFarmingGame.Domains.Response;
using TheFarmingGame.Services;

namespace TheFarmingGame.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IAuthenticationService _authorizationService;
        private readonly IUserService _userService;

        public AuthenticationController(ILogger<AuthenticationController> logger, IAuthenticationService authorizationService, IUserService userService)
        {
            _logger = logger;
            _authorizationService = authorizationService;
            _userService = userService;
        }

        [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (request.UserName == null || request.Password == null || request.Alias == null)
            {
                return BadRequest("Empty username or password.");
            }
            User user = await _authorizationService.Register(request.UserName, request.Password, request.Alias);
            return Ok(user);
        }

        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (request.UserName == null || request.Password == null)
            {
                return BadRequest("Empty username or password.");
            }
            User user = await _authorizationService.Login();
            UserResponse userResponse = new UserResponse()
            {
                Alias = user.Alias,
                Lands = user.Lands,
                Money = user.Money,
                ProtectAmount = user.ProtectAmount,
                StealAmount = user.StealAmount
            };
            return Ok(userResponse);
        }

       
    }
}