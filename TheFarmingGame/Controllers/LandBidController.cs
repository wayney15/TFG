using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheFarmingGame.Domains;
using TheFarmingGame.Domains.Response;
using TheFarmingGame.Services;

namespace TheFarmingGame.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LandBidController : ControllerBase
    {
        private readonly ILandBidService _landBidService;
        private readonly ILandService _landService;
        private readonly IUserService _userService;
        private readonly IBidService _bidService;
        private readonly ILogger<LandBidController> _logger;
        public LandBidController(ILogger<LandBidController> logger, ILandBidService landBidService, ILandService landService, IUserService userService, IBidService bidService)
        {
            _landBidService = landBidService;
            _landService = landService;    
            _userService = userService;
            _bidService = bidService;
            _logger = logger;
        }

        [Authorize]
        [Route("GetAllActiveLandBids")]
        [HttpGet]
        public async Task<IActionResult> GetAllActiveLandBids()
        {
            var userId = User?.Claims?.FirstOrDefault(c => c.Type == "UserId")?.Value;
            if (userId == null)
            {
                return NotFound("Current user not found.");
            }
            var returnList = await _landBidService.GetAllActiveLandBidsAsync();
            return Ok(returnList);
        }

        [Authorize]
        [Route("GetAllInActiveLandBids")]
        [HttpGet]
        public async Task<IActionResult> GetAllInActiveLandBids()
        {
            var userId = User?.Claims?.FirstOrDefault(c => c.Type == "UserId")?.Value;
            if (userId == null)
            {
                return NotFound("Current user not found.");
            }
            var returnList = await _landBidService.GetAllInActiveLandBidsAsync();
            return Ok(returnList);
        }

        [Authorize]
        [Route("GetAllUserParticipatedLandBids")]
        [HttpGet]
        public async Task<IActionResult> GetAllUserParticipatedLandBids()
        {
            var userId = User?.Claims?.FirstOrDefault(c => c.Type == "UserId")?.Value;
            if (userId == null)
            {
                return NotFound("Current user not found.");
            }
            var user = await _userService.GetUserByIdAsync(int.Parse(userId));
            if (user ==  null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Not authorized.");
            }

            // get all user bids
            var bidList = await _bidService.GetAllBidsAsync();
            if(bidList == null)
            {
                return Ok(new List<LandBid>());
            }

            var userBids = bidList.Where(b => b.UserId == user.Id).GroupBy(b => b.LandBidId).Select(b => b.Key).ToList();

            var returnList = await _landBidService.GetAllLandBidsByIdsAsync(userBids);
            return Ok(returnList);
        }
    }
}
