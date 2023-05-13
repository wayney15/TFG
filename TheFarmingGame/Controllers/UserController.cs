using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TheFarmingGame.Domains;
using TheFarmingGame.Domains.Requests;
using TheFarmingGame.Domains.Response;
using TheFarmingGame.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TheFarmingGame.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class userController : ControllerBase
    {
        private readonly ILogger<userController> _logger;
        private readonly IUserService _userService;

        public userController(ILogger<userController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [Authorize]
        [Route("GetAllUsers")]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var userId = User?.Claims?.FirstOrDefault(c => c.Type == "UserId")?.Value;
            if(userId == null)
            {
                return NotFound("Current user not found.");
            }
            var userList = await _userService.GetAllUsersExceptSelfAsync(int.Parse(userId));
            if(userList.Count() == 0)
            {
                return NotFound("You are the only user");
            }
            var returnList = new List<Object>();
            foreach(User u in userList)
            {
                returnList.Add(new {Alias = u.Alias});
            }
            return Ok(returnList);
        }

        [Authorize]
        [Route("GetCurrentUser")]
        [HttpGet]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userId = User?.Claims?.FirstOrDefault(c => c.Type == "UserId")?.Value;
            if(userId == null)
            {
                return NotFound("Current user not found.");
            }
            User user = await _userService.GetUserByIdAsync(int.Parse(userId));
            UserResponse userResponse = new UserResponse()
            {
                Alias = user.Alias,
                Lands = user.Lands,
                Money = user.Money,
                ProtectAmount = user.ProtectAmount,
                StealAmount = user.StealAmount,
            };
            return Ok(userResponse);
        }            

        [Authorize]
        [Route("Leaderboard")]
        [HttpGet]
        public async Task<IActionResult> Leaderboard()
        {
            var userId = User?.Claims?.FirstOrDefault(c => c.Type == "UserId")?.Value;
            if(userId == null)
            {
                return NotFound("Current user not found.");
            }
            var userList = await _userService.GetAllUsersAsync();
            if(userList.Count() == 0)
            {
                return NotFound("You are the only user");
            }
            var returnList = new List<LeaderboardResponse>();
            foreach(User u in userList)
            {
                LeaderboardResponse lr = new LeaderboardResponse();
                lr.Alias = u.Alias;
                lr.Money = u.Money;
                returnList.Add(lr);
            }
            returnList.Sort((x, y) => y.Money.CompareTo(x.Money));

            return Ok(returnList);
        }

        // GET api/<userController>/5
        [Authorize]
        [HttpGet]
        [Route("GetUserById")]
        public async Task<IActionResult> GetUserById([FromQuery] int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if(user == null)
            {
                return NotFound("User not found.");
            }
            UserResponse userResponse = new UserResponse()
            {
                Alias = user.Alias,
                Lands = user.Lands,
                Money = user.Money,
                ProtectAmount = user.ProtectAmount,
                StealAmount = user.StealAmount,
            };

            return Ok(userResponse);
        }


        // POST api/<userController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<userController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<userController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
