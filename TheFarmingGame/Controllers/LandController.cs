using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;
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
    public class landController : ControllerBase
    {
        private readonly ILandService _landService;

        public landController(ILandService landService)
        {
            _landService = landService;
        }

        [Authorize]
        [Route("GetAllLands")]
        [HttpGet]
        public async Task<IActionResult> GetAllLands()
        {
            var landList = await _landService.GetAllLandAsync();
            if(landList == null)
            {
                return Ok();
            }
            var returnList = new List<Object>();
            foreach(Land l in landList)
            {
                returnList.Add(new {Alias = l.Alias});
            }
            return Ok(returnList);
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
