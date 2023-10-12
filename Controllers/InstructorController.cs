using Microsoft.AspNetCore.Mvc;

namespace iLearn.Controllers
{
    public class InstructorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
