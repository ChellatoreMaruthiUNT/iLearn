using Microsoft.AspNetCore.Mvc;

namespace iLearn.Controllers
{
    public class LandingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
