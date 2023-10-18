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
    public class CourseModel : PageModel
    {
        private readonly ICourseService _courseService;
        private readonly UserManager<ApplicationUser> _userManager;

        public CourseModel(ICourseService courseService, UserManager<ApplicationUser> userManager)
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
        }
        public void OnGet()
        {
            
        }

        public async Task<IActionResult> OnPost()
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            if (user != null)
            {
                try
                {
                    var course = new Course()
                    {
                        CourseTitle = Input.Title,
                        CourseDescription = Input.Description,
                        CreatedOn = DateTime.Now,
                        ModifiedOn = DateTime.Now,
                        IsCertificationProvided = Input.IsCertificationProvided,
                        IsEvaluationRequired = Input.IsAssessmentRequired,
                        // Getting value from session set on logn method
                        InstructorId = user.InstructorId
                    };
                    _courseService.CreateCourse(course);
                    SuccessMessage = "Course creation successful.";

                    // Clear form data
                    Input = new InputModel();
                    
                }
                catch(Exception ex)
                {
                    ErrorMessage = "Could not create course.";
                }                
            }
            return RedirectToPage();
        }
    }
}
