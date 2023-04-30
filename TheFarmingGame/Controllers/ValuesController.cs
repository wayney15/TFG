using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheFarmingGame.Domains;

namespace TheFarmingGame.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersAPIController : Controller
    {
        // GET: api/GetAllUsers 
        [HttpGet]
        public IEnumerable<User> GetAllUsers()
        {
            List<User> students = new List<User>
           {
           new User{
                              Id = 1234,
                              UserName = "Thomas",
                              Password = "123456",
                              Alias = "TT",
                              Money = 12.34M
                              },
           new User{
                              Id = 1244,
                              UserName = "Jack",
                              Password = "12345",
                              Alias = "JJ",
                              Money = 12.44M
                              },
           };
            return students;
        }
    }
}
