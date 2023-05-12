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

        // POST api/<landController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<landController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<landController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
