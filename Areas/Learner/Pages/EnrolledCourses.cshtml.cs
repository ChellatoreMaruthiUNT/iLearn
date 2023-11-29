using iLearn.Areas.Identity.Data;
using iLearn.Areas.Instructor.Data;
using iLearn.iLearnDbModels;
using iLearn.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace iLearn.Views.Learner
{
    public class EnrolledCoursesModel : PageModel
    {
        private readonly ICourseService _courseService;
        private readonly UserManager<ApplicationUser> _userManager;

        public EnrolledCoursesModel(ICourseService courseService, UserManager<ApplicationUser> userManager)
        {
            _courseService = courseService;
            _userManager = userManager;
        }
        [BindProperty]
        public List<UserCourseMapping> UserCourseMappings { get; set; } = new List<UserCourseMapping>();

        public async void OnGet()
        {
            UserCourseMappings = _courseService.GetCoursesRegisteredByUser(User.Identity.Name);
        }

        public async void OnPost()
        {         
        }
    }
}
