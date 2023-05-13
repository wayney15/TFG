using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheFarmingGame.Domains;
using TheFarmingGame.Domains.Requests;
using TheFarmingGame.Domains.Response;
using TheFarmingGame.Services;

namespace TheFarmingGame.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BidController : ControllerBase
    {
        private readonly ILogger<BidController> _logger;
        private readonly IUserService _userService;
        private readonly IBidService _bidService;
        private readonly ILandBidService _landBidService;
        public BidController(ILogger<BidController> logger, IUserService userService, IBidService bidService, ILandBidService landBidService)
        {
            _logger = logger;
            _userService = userService;
            _bidService = bidService;
            _landBidService = landBidService;
        }

        [Authorize]
        [Route("MakeBid")]
        [HttpPost]
        public async Task<IActionResult> MakeBid([FromBody] BidRequest request)
        {
            if (request.LandId <= 0)
                return BadRequest("Incorrect land id.");

            if (request.Amount <= 0)
                return BadRequest("Incorrect amount.");

            // need input validation
            // get user id
            var userId = User?.Claims?.FirstOrDefault(c => c.Type == "UserId")?.Value;
            if (userId == null)
                return StatusCode(StatusCodes.Status401Unauthorized, "Not authorized.");

            var user = await _userService.GetUserByIdAsync(int.Parse(userId));
            if (user == null)
                return NotFound("Current user not found.");

            // see if landId is on bid
            var activeLandBids = await _landBidService.GetAllActiveLandBidsAsync();
            if (activeLandBids == null)
                return BadRequest("Nothing is on bid");
            var landBid = activeLandBids.Where(l => l.LandId == request.LandId).FirstOrDefault();
            if (landBid == null)
                return BadRequest("Land is not on bid");

            // see if the amount is larger than current max bid
            var currentBids = await _bidService.GetBidsByLandBidIdAsync(landBid.Id);
            if (currentBids.Count() != 0)
            {
                var max = currentBids.Max(b => b.BidAmount);
                if (request.Amount <= max)
                    return BadRequest("You cannot bid lower than current high.");
            }

            // see if user has enough to make the bid
            if (user.Money < request.Amount)
            {
                return BadRequest("You don't have enough money.");
            }
            // now the bid is valid, add it to db
            try
            {
                await _bidService.AddBidAsync(new Bid { LandBidId = landBid.Id, UserId = user.Id, BidAmount = request.Amount });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding the entity.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while making the bid.");
            }
            return Ok("Bid made.");
        }

        [Authorize]
        [Route("GetBidsByLandBidId")]
        [HttpGet]
        public async Task<IActionResult> GetBidsByLandBidId([FromQuery] int landBidId)
        {
            if (landBidId <= 0)
                return BadRequest("Incorrect amount.");

            // get user id
            var userId = User?.Claims?.FirstOrDefault(c => c.Type == "UserId")?.Value;
            if (userId == null)
                return StatusCode(StatusCodes.Status401Unauthorized, "Not authorized.");

            var user = await _userService.GetUserByIdAsync(int.Parse(userId));
            if (user == null)
                return NotFound("Current user not found.");

            var allBids = await _bidService.GetBidsByLandBidIdAsync(landBidId);
            allBids.OrderByDescending(b => b.BidAmount).ToList();

            var returnList = new List<BidResponse>();
            foreach (var b in allBids)
            {
                var cur_user = await _userService.GetUserByIdAsync(b.UserId);
                var res = new BidResponse()
                {
                    BidAmount = b.BidAmount,
                    UserAlias = user.Alias
                };
                returnList.Add(res);
            }
            return Ok(returnList);
        }
    }
}
