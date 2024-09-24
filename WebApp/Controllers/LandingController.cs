using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class LandingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
