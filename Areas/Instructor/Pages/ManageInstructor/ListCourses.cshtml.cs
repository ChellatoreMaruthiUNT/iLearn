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

namespace iLearn.Views.Instructor
{
    public class ListCoursesModel : PageModel
    {
        private readonly ICourseService _courseService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ListCoursesModel(ICourseService courseService, UserManager<ApplicationUser> userManager)
        {
            _courseService = courseService;
            _userManager = userManager;
        }
        [BindProperty]
        public List<CourseViewModel> CoursesList { get; set; } = new List<CourseViewModel>();

        public async void OnGet()
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            var courses = _courseService.GetCoursesByIntructor(user.InstructorId);
            if (courses != null)
            {
                foreach (var item in courses)
                {
                    CoursesList.Add(new CourseViewModel()
                    {
                        CourseCode = item.CourseCode,
                        CourseTitle = item.CourseTitle,
                        CourseDescription = item.CourseDescription
                    });
                }
            }
        }

        public async void OnPost()
        {         
        }
    }
}
