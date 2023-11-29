using iLearn.Areas.Identity.Data;
using iLearn.Areas.Instructor.Data;
using iLearn.iLearnDbModels;
using iLearn.Services;
using iLearn.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace iLearn.Views.Learner
{
    public class CourseProgressModel : PageModel
    {
        private readonly ICourseService _courseService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserService _userService;

        public CourseProgressModel(ICourseService courseService, UserManager<ApplicationUser> userManager, IUserService userService)
        {
            _courseService = courseService;
            _userManager = userManager;
            _userService = userService;
        }
        [BindProperty]
        public UserCourseMapping UserCourse { get; set; } 

        public void OnGet(string courseCode)
        {
            UserCourse = _userService.GetCourseProgress(User.Identity.Name, courseCode);
        }

        public async void OnPost()
        {         
        }
    }
}
