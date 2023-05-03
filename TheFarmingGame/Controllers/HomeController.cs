using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace TheFarmingGame.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("[controller]")]
        public ContentResult Home()
        {
            var html = System.IO.File.ReadAllText("Views/Home.cshtml");
            return base.Content(html, "text/html");
        }

        [HttpGet("Login")]
        public ContentResult Login()
        {
            var html = System.IO.File.ReadAllText("Views/Login.cshtml");
            return base.Content(html, "text/html");
        }

        [HttpGet("Register")]
        public ContentResult Register()
        {
            var html = System.IO.File.ReadAllText("Views/Register.cshtml");
            return base.Content(html, "text/html");
        }
    }
}
