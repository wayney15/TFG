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
        private readonly IUserService _userService;

        public landController(ILandService landService, IUserService userService)
        {
            _landService = landService;
            _userService = userService;
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
            var returnList = new List<LandResponse>();
            foreach(Land l in landList)
            {
                LandResponse lr = new LandResponse();
                lr.Alias = l.Alias;
                lr.Level = l.Level;
                lr.Plant = l.Plant;
                lr.HarvestTime = l.HarvestTime;
                lr.IsProtected = l.IsProtected;
                returnList.Add(lr);
            }
            return Ok(returnList);
        }

        [Authorize]
        [Route("GetCurrentUserLands")]
        [HttpGet]
        public async Task<IActionResult> GetCurrentUserLands()
        {
            var userId = User?.Claims?.FirstOrDefault(c => c.Type == "UserId")?.Value;
            if (userId == null)
            {
                return NotFound("Current user not found.");
            }
            var landList = await _landService.GetLandByUserIdAsync(Convert.ToInt32(userId));
            if(landList == null)
            {
                return Ok();
            }
            var returnList = new List<LandResponse>();
            foreach(Land l in landList)
            {
                LandResponse lr = new LandResponse();
                lr.Id = l.Id;
                lr.Alias = l.Alias;
                lr.Level = l.Level;
                lr.Plant = l.Plant;
                lr.HarvestTime = l.HarvestTime;
                lr.IsProtected = l.IsProtected;
                returnList.Add(lr);
            }
            return Ok(returnList);
        }

        [Authorize]
        [HttpPost]
        [Route("Plant")]
        public async Task<IActionResult> Plant([FromBody] int landId, int plantId)
        {
            // check if user is authenticated
            var userId = User?.Claims?.FirstOrDefault(c => c.Type == "UserId")?.Value;
            if (userId == null)
            {
                return NotFound("Current user not found.");
            }
            var user = await _userService.GetUserByIdAsync(int.Parse(userId));
            if (user == null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Not authorized.");
            }

            // see if the user owns the land
            if (user.Lands == null || !user.Lands.Any(l => l.Id == landId))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Not authorized.");
            }
            // check if plantId is valid
            if (plantId<=1 && plantId >= 4){
                return BadRequest("Invalid plant id.");
            }

            // check if the land has anything planted
            var targetLand = user.Lands.Where(l => l.Id == landId).First();
            if (targetLand.Plant != 0)
            {
                return BadRequest("Land has plants already.");
            }

            // now we can plant the land
            targetLand.Plant = plantId;
            switch (plantId)
            {
                case 1:
                    targetLand.HarvestTime = DateTime.Now.AddMinutes(1);
                    break;
                case 2:
                    targetLand.HarvestTime = DateTime.Now.AddMinutes(3);
                    break;
                case 3:
                    targetLand.HarvestTime = DateTime.Now.AddMinutes(5);
                    break;
                case 4:
                    targetLand.HarvestTime = DateTime.Now.AddMinutes(10);
                    break;
            }

            //update db
            await _landService.UpdateLand(targetLand);
            return Ok("Plant successful!");

        }

        [Authorize]
        [HttpPost]
        [Route("Harvest")]
        public async Task<IActionResult> Harvest([FromBody] int landId)
        {
            // check if user is authenticated
            var userId = User?.Claims?.FirstOrDefault(c => c.Type == "UserId")?.Value;
            if (userId == null)
            {
                return NotFound("Current user not found.");
            }
            var user = await _userService.GetUserByIdAsync(int.Parse(userId));
            if (user == null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Not authorized.");
            }

            // see if the user owns the land
            if (user.Lands == null || !user.Lands.Any(l => l.Id == landId))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Not authorized.");
            }

            // check if the land has anything planted
            var targetLand = user.Lands.Where(l => l.Id == landId).First();
            if (targetLand.Plant == 0)
            {
                return BadRequest("Land has no plants.");
            }

            // check if the plant is ready to harvest
            if (targetLand.HarvestTime > DateTime.Now)
            {
                return BadRequest("Plant is not ready to harvest yet.");
            }

            // add harvested gain 
            var gain = 0;
            switch (targetLand.Plant)
            {
                case 1:
                    gain = 1000;
                    break;
                case 2:
                    gain = 4000;
                    break;
                case 3:
                    gain = 7000;
                    break;
                case 4:
                    gain = 20000;
                    break;
            }
            user.Money += gain;
            await _userService.UpdateUser(user);
            targetLand.Plant = 0;
            await _landService.UpdateLand(targetLand);
            
            return Ok("Harvest successful, you gained " + gain.ToString() + "!");

        }

        // DELETE api/<landController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
