using Microsoft.AspNetCore.Mvc;
﻿using Microsoft.AspNetCore.Authorization;
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
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if(request.username == null || request.password == null || request.alias == null)
            {
                return BadRequest("Empty username, password, or alias.");
            }
            var valid = false;
            // need input validation _!+=?
            if (request.password.Contains("_") || request.password.Contains("!") || request.password.Contains("+") || request.password.Contains("=") || request.password.Contains("?"))
            {
                valid = true;
            }
            if (!valid)
            {
                return BadRequest("Password must include at least one special character");
            }
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
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromQuery] LoginRequest request)
        {
            if (request.username == null || request.password == null)
            {
                return BadRequest("Empty username or password.");
            }
            var user = await _userService.LoginAsync(request.username, request.password);
            if (user == null)
            {
                _logger.LogError("Failed to login user: " + request.username + ".");
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
                expires: DateTime.Now.AddMinutes(10), 
                signingCredentials: signinCredentials);
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

            AuthenticateResponse userResponse = new AuthenticateResponse(user, tokenString);
            return Ok(userResponse);
        }

       
    }
}