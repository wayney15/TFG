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
        private readonly IGameplayService _gameplayService;
        private readonly IUserService _userService;

        [Route("UserInfo")]
        [HttpPost]
        public async Task<IActionResult> UserInfo(string Id)
        {
            String test = await _gameplayService.UserInfo(Id);
            return Ok(test);
        }

        [Route("ListUsers")]
        [HttpPost]
        public async Task<IActionResult> ListUsers()
        {
            String test = await _gameplayService.ListUsers();
            return Ok(test);
        }

        [Route("ListLands")]
        [HttpPost]
        public async Task<IActionResult> ListLands()
        {
            String test = await _gameplayService.ListLands();
            return Ok(test);
        }

        [Route("ListBids")]
        [HttpPost]
        public async Task<IActionResult> ListBids()
        {
            String test = await _gameplayService.ListBids();
            return Ok(test);
        }

        [Route("ListUserBids")]
        [HttpPost]
        public async Task<IActionResult> ListUserBids(string Id)
        {
            String test = await _gameplayService.ListUserBids(Id);
            return Ok(test);
        }

        [Route("ListUserLand")]
        [HttpPost]
        public async Task<IActionResult> ListUserLands(string Id)
        {
            String test = await _gameplayService.ListUserLands(Id);
            return Ok(test);
        }
    }
}