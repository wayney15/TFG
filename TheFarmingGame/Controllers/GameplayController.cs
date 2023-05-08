using Microsoft.AspNetCore.Mvc;
using TheFarmingGame.Domains;
using TheFarmingGame.Domains.Requests;
using TheFarmingGame.Services;

namespace TheFarmingGame.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameplayController : ControllerBase
    {
        private readonly IBidService _bidService;
        private readonly ILandService _landService;
        private readonly IUserService _userService;

        [Route("UserInfo")]
        [HttpPost]
        public async Task<IActionResult> UserInfo(string Id)
        {
            String test = await _userService.UserInfo(Id);
            return Ok(test);
        }

        [Route("ListUsers")]
        [HttpPost]
        public async Task<IActionResult> ListUsers()
        {
            String test = await _userService.ListUsers();
            return Ok(test);
        }

        [Route("ListLands")]
        [HttpPost]
        public async Task<IActionResult> ListLands()
        {
            String test = await _landService.ListLands();
            return Ok(test);
        }

        [Route("ListBids")]
        [HttpPost]
        public async Task<IActionResult> ListBids()
        {
            String test = await _bidService.ListBids();
            return Ok(test);
        }

        [Route("ListUserBids")]
        [HttpPost]
        public async Task<IActionResult> ListUserBids(string Id)
        {
            String test = await _bidService.ListUserBids(Id);
            return Ok(test);
        }

        [Route("ListUserLands")]
        [HttpPost]
        public async Task<IActionResult> ListUserLands(string Id)
        {
            String test = await _landService.ListUserLands(Id);
            return Ok(test);
        }
    }
}