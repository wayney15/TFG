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
    public class bidController : ControllerBase
    {
        private readonly IBidService _bidService;

        public bidController(IBidService bidService)
        {
            _bidService = bidService;
        }

        [Authorize]
        [Route("GetAllBids")]
        [HttpGet]
        public async Task<IActionResult> GetAllBids()
        {
            var bidList = await _bidService.GetAllBidAsync();
            if(bidList == null)
            {
                return Ok();
            }
            var returnList = new List<BidResponse>();
            foreach(Bid b in bidList)
            {
                BidResponse br = new BidResponse();
                br.LandAlias = b.LandAlias;
                br.UserAlias = b.UserAlias;
                br.BidAmount = b.BidAmount;
                returnList.Add(br);
            }
            return Ok(returnList);
        }

        // POST api/<bidController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<bidController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<bidController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
