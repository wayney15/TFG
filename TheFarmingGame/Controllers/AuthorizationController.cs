using Microsoft.AspNetCore.Mvc;
using TheFarmingGame.Domains;
using TheFarmingGame.Services;

namespace TheFarmingGame.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorizationController : ControllerBase
    {
        private readonly ILogger<AuthorizationController> _logger;
        private readonly IAuthorizationService _authorizationService;

        public AuthorizationController(ILogger<AuthorizationController> logger, IAuthorizationService authorizationService)
        {
            _logger = logger;
            _authorizationService = authorizationService;
        }

        [HttpPost]
        public String Register()
        {
            // call service authorization functions
            return _authorizationService.Register();
        }

        [HttpPost]
        public User Login()
        {
            return _authorizationService.Login();
        }
    }
}