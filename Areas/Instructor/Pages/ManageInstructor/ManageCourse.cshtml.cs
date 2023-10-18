using iLearn.Areas.Identity.Data;
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
    public class ManageCourseModel : PageModel
    {
        private readonly ICourseService _courseService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ManageCourseModel(ICourseService courseService, UserManager<ApplicationUser> userManager)
        {
            _courseService = courseService;
            _userManager = userManager;
        }
        [BindProperty]
        public InputModel Input { get; set; }
        public string ReturnUrl { get; set; }
        [TempData]
        public string SuccessMessage { get; set; }
        [TempData]
        public string ErrorMessage { get; set; }
        public class InputModel
        {
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Course Title")]
            public string Title { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Course Description")]
            public string Description { get; set; }

            
            [Display(Name = "Is Certitifate Provided")]
            public bool IsCertificationProvided { get; set; }

            [Display(Name = "Is Assessment Required")]
            public bool IsAssessmentRequired { get; set; }
            public string CourseCode { get; set; }
        }
        public void OnGet(string courseCode)
        {
            var course = _courseService.GetCourseByCourseCode(courseCode);
            if(course != null) {
                Input = new InputModel()
                {
                    CourseCode = courseCode,
                    Title = course.CourseTitle,
                    Description = course.CourseDescription,
                    IsAssessmentRequired = course.IsEvaluationRequired ?? false,
                    IsCertificationProvided = course.IsCertificationProvided ?? false
                };
            }
            
        }

        public async Task<IActionResult> OnPost()
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(User.Identity.Name);
                if (user != null)
                {
                    var course = new Course()
                    {
                        CourseCode = Input.CourseCode,
                        CourseTitle = Input.Title,
                        CourseDescription = Input.Description,
                        CreatedOn = DateTime.Now,
                        ModifiedOn = DateTime.Now,
                        IsCertificationProvided = Input.IsCertificationProvided,
                        IsEvaluationRequired = Input.IsAssessmentRequired,
                        // Getting value from session set on logn method
                        InstructorId = user.InstructorId
                    };
                    _courseService.UpdateCourse(course);
                }
                SuccessMessage = "Course edit successful.";
            }
            catch(Exception ex)
            {
                ErrorMessage = "Could not edit course. Please try again later.";
            }
            return RedirectToPage();
        }
    }
}
