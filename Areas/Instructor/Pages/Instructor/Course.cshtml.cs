using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace iLearn.Views.Instructor
{
    public class CourseModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; }
        public string ReturnUrl { get; set; }
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
    }
}
