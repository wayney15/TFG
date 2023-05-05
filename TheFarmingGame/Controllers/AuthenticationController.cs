using Microsoft.AspNetCore.Mvc;
using TheFarmingGame.Domains;
using TheFarmingGame.Services;

namespace TheFarmingGame.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IAuthenticationService _authorizationService;

        public AuthenticationController(ILogger<AuthenticationController> logger, IAuthenticationService authorizationService)
        {
            _logger = logger;
            _authorizationService = authorizationService;
        }

        [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> Register()
        {
            // call service authorization functions
            //_authorizationService.Register();
            string test = await _authorizationService.Register();
            return Ok(test);
        }

        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login()
        {
            User user = await _authorizationService.Login();
            return Ok(user);
        }
    }
}