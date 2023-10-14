using iLearn.Areas.Instructor.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace iLearn.Views.Instructor
{
    public class SectionsModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; }
        [BindProperty]
        [Required]
        [Display(Name = "Section Material")]
        public IFormFile SectionFile { get; set; }
        public string ReturnUrl { get; set; }
        public List<CourseViewModel> CoursesList { get; set; } = new List<CourseViewModel>()
        {
            new CourseViewModel()
            {
                CourseCode = "Test",
                CourseTitle = "Manual Testing Course",
            }
        };

        public class InputModel
        {
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Section Title")]
            public string Title { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Section Description")]
            public string Description { get; set; }
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Section Duration")]
            public string Duration { get; set; }            

            [Required]
            [Display(Name = "Select Course")]
            public string CourseCodeSelected { get; set; }
        }
        public void OnGet()
        {
            
        }

        public void OnPost()
        {
            if (SectionFile != null && SectionFile.Length > 0)
            {
                // Process the file, save, or perform other actions
                // For example, save to a specific location or process the file contents
            }
        }
    }
}
