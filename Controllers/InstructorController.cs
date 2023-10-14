using iLearn.Areas.Instructor.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace iLearn.Controllers
{
    public class InstructorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public void PostCourseSections([FromBody] EvaluationFormModel FormData)
        {

        }
    }
}
