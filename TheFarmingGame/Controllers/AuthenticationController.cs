using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
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
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public AuthenticationController(ILogger<AuthenticationController> logger, IUserService userService, IConfiguration configuration)
        {
            _logger = logger;
            _userService = userService;
            _configuration = configuration;
        }

        [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if(request.username == null || request.password == null || request.alias == null)
            {
                return BadRequest("Empty username, password, or alias.");
            }
            // need input validation
            try
            {
                await _userService.RegisterAsync(request.username, request.password, request.alias);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding the entity.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while registering.");
            }
            return Ok();
        }

        [Route("Login")]
        [HttpGet]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (request.UserName == null || request.Password == null)
            {
                return BadRequest("Empty username or password.");
            }
            var user = await _userService.LoginAsync(request.UserName, request.Password);
            if (user == null)
            {
                _logger.LogError("Failed to login user: " + request.UserName + ".");
                return StatusCode(StatusCodes.Status401Unauthorized, "Incorrect username or password.");
            }
            var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", user.Id.ToString()),
                        new Claim("UserAlias", user.Alias)
                    };
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(issuer: _configuration["JWT:ValidIssuer"], 
                audience: _configuration["JWT:ValidAudience"], 
                claims: claims, 
                expires: DateTime.Now.AddMinutes(30), 
                signingCredentials: signinCredentials);
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

            UserResponse userResponse = new UserResponse()
            {
                Alias = user.Alias,
                Lands = user.Lands,
                Money = user.Money,
                ProtectAmount = user.ProtectAmount,
                StealAmount = user.StealAmount,
                token = tokenString
                
            };
            return Ok(userResponse);
        }
    }
}