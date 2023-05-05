using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace TheFarmingGame.Controllers
{
    public class HomeController : Controller
    {
        [Route("[controller]")]
        public ContentResult Home()
        {
            var html = System.IO.File.ReadAllText("Views/Home.cshtml");
            return base.Content(html, "text/html");
        }
    }
}
