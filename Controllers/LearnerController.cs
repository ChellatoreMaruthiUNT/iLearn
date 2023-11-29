using iLearn.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;


namespace iLearn.Controllers
{
    public class LearnerController : Controller
    {
        private readonly IUserService _userService;
        private readonly ICompositeViewEngine _viewEngine;
        private readonly IServiceProvider _serviceProvider;
        private readonly ICourseService _courseService;
        public LearnerController(IUserService userService, ICompositeViewEngine viewEngine, IServiceProvider serviceProvider, ICourseService courseService)
        {
            _userService = userService;
            _viewEngine = viewEngine;
            _serviceProvider = serviceProvider;
            _courseService = courseService;
        }
        [HttpPost]
        public IActionResult EnrollCourse(string courseCode)
        {
            _userService.EnrollCourse(courseCode, User.Identity.Name);
            var courseProgressUrl = "/Learner/CourseProgress?courseCode=" + courseCode + "&success=true";
            return Json(new { redirectUrl = courseProgressUrl });
        }

        public IActionResult GetCertificate(string courseTitle)
        {
            return PartialView("_DownloadCertificate", courseTitle);
        }

        [HttpGet]
        public IActionResult SetReminder(string courseCode, bool turnOnNotification)
        {
            if(turnOnNotification)
            {
                _userService.AddNotification(courseCode, User.Identity.Name);
            }
            else
            {
                _userService.RemoveNotification(courseCode, User.Identity.Name);
            }
            return Ok();
        }
    }
}
