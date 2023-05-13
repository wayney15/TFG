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

            var targetLand = await _landService.GetLandByIdAsync(landId);
            if(targetLand == null)
            {
                return BadRequest("Invalid plant id.");
            }
            if(targetLand.UserId != user.Id)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "This is not your land.");
            }

            // see if the user owns the land
            // check if plantId is valid
            if (plantId<=1 && plantId >= 4){
                return BadRequest("Invalid plant id.");
            }

            // check if the land has anything planted
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


            var targetLand = await _landService.GetLandByIdAsync(landId);
            if (targetLand == null)
            {
                return BadRequest("Invalid plant id.");
            }
            if (targetLand.UserId != user.Id)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "This is not your land.");
            }

            // check if the land has anything planted
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
            targetLand.HarvestTime = null;
            targetLand.IsProtected = false;
            await _landService.UpdateLand(targetLand);
            
            return Ok("Harvest successful, you gained " + gain.ToString() + "!");
        }

        [Authorize]
        [HttpPost]
        [Route("Steal")]
        public async Task<IActionResult> Steal([FromBody] int landId)
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

            var targetLand = await _landService.GetLandByIdAsync(landId);
            if (targetLand == null)
            {
                return BadRequest("Invalid plant id.");
            }
            if (targetLand.UserId == user.Id)
            {
                return BadRequest("You don't have to steal from your own lands.");
            }

            // check if the land has anything planted
            if (targetLand.Plant == 0)
            {
                return BadRequest("Land has no plants.");
            }

            // check if the plant is ready to harvest
            if (targetLand.HarvestTime > DateTime.Now)
            {
                return BadRequest("Plant is not ready to steal yet.");
            }

            // check if the plant is ready to harvest
            if (targetLand.IsProtected == true)
            {
                return BadRequest("Land is protected.");
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
            targetLand.HarvestTime = null;
            await _landService.UpdateLand(targetLand);

            return Ok("Stealing successful, you gained " + gain.ToString() + "!");
        }

        [Authorize]
        [HttpPost]
        [Route("Protect")]
        public async Task<IActionResult> Protect([FromBody] int landId)
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

            var targetLand = await _landService.GetLandByIdAsync(landId);
            if (targetLand == null)
            {
                return BadRequest("Invalid plant id.");
            }
            if (targetLand.UserId != user.Id)
            {
                return BadRequest("This is not your land.");
            }

            // check if the land has anything planted
            if (targetLand.Plant == 0)
            {
                return BadRequest("Land has no plants.");
            }

            if(targetLand.IsProtected == true)
            {
                return BadRequest("Land is protected already.");
            }

            if(user.ProtectAmount < 1)
            {
                return BadRequest("You have no protection left.");
            }

            targetLand.IsProtected = true;
            await _landService.UpdateLand(targetLand);
            user.ProtectAmount -= 1;
            await _userService.UpdateUser(user);

            return Ok("Protection used.");
        }
    }
}
