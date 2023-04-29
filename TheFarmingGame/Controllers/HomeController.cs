using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace TheFarmingGame.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Welcome()
        {
            return View("../Views/Home/Welcome.cshtml");
        }
    }
}
