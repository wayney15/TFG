using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace TheFarmingGame.Controllers
{
    public class HomeController : Controller
    {
        [Route("[controller]")]
        public ActionResult Home()
        {
            return View("~/Views/Home.cshtml");
        }

        [Route("Login")]
        public ActionResult Login()
        {
            return View("~/Views/Login.cshtml");
        }

        [Route("Register")]
        public ActionResult Register()
        {
            return View("~/Views/Register.cshtml");
        }
    }
}
