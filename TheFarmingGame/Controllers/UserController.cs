using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IUserService _userService;

        public userController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/<userController>
        [HttpGet]
        public async Task<IActionResult> GetUserById([FromBody] LoginRequest request)
        {
            if (request.UserName == null || request.Password == null)
            {
                return BadRequest("Empty username or password.");
            }
            return Ok();
        }

        // GET api/<userController>/5
        [HttpGet("{id}")]
        public UserResponse Get(string id)
        {
            // UserResponse userResponse = _userService.Get(id)
            UserResponse userResponse= new UserResponse();
            userResponse.Alias = "John";
            userResponse.Money = 1234;
            userResponse.StealAmount = 12345;
            userResponse.ProtectAmount = 123456;
            return userResponse;
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
