using Microsoft.AspNetCore.Mvc;
using TheFarmingGame.Domains;
using TheFarmingGame.Domains.Requests;
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
        public async Task<IActionResult> Register(string UserName, string Password, string Alias)
        {
            // call service authorization functions
            //_authorizationService.Register();
            string test = await _authorizationService.Register(UserName, Password, Alias);
            return Ok(test);
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
            return Ok(user);
        }
    }
}