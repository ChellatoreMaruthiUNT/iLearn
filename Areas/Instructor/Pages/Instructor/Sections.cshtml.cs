using iLearn.Areas.Identity.Data;
using iLearn.Areas.Instructor.Data;
using iLearn.iLearnDbModels;
using iLearn.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace iLearn.Views.Instructor
{
    public class SectionsModel : PageModel
    {
        private readonly IFileStorageService _fileStorageService;
        private readonly ISectionsService _sectionsService;
        private readonly ICourseService _courseService;
        private readonly UserManager<ApplicationUser> _userManager;

        public SectionsModel(IFileStorageService fileStorageService, ISectionsService sectionsService,
            ICourseService courseService, UserManager<ApplicationUser> userManager)
        {
            _fileStorageService = fileStorageService;
            _sectionsService = sectionsService;
            _courseService = courseService;
            _userManager = userManager;
        }
        [BindProperty]
        public InputModel Input { get; set; }
        [BindProperty]
        [Required]
        [Display(Name = "Section Material")]
        public IFormFile SectionFile { get; set; }
        public string ReturnUrl { get; set; }
        public List<CourseViewModel> CoursesList { get; set; } = new List<CourseViewModel>();
        [TempData]
        public string SuccessMessage { get; set; }
        [TempData]
        public string ErrorMessage { get; set; }
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
            public int Duration { get; set; }            

            [Required]
            [Display(Name = "Select Course")]
            public string CourseCodeSelected { get; set; }
            [Required]
            [Display(Name = "Map Evaluation by selecting Yes and filling details in popup")]
            public bool IsEvaluationMandatory { get; set; }            
            public int EvaluationId { get; set; }
            [Display(Name = "Yes")]
            public bool IsOptionYesSelected { get; set; } = false;

            [Display(Name = "NO")]
            public bool IsOptionNoSelected { get; set; } = false;
        }
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
                        CourseTitle = item.CourseTitle
                    });
                }
            }
        }

        public IActionResult OnPost()
        {
            string fileNameFromAzure = string.Empty;
            try
            {
                if (SectionFile != null && SectionFile.Length > 0)
                {
                    string tempDir = Path.GetTempPath();
                    var fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(SectionFile.FileName);
                    string destinationFilePath = Path.Combine(Path.GetTempPath(), fileName);
                    // log we can remove later
                    _fileStorageService.LogMessage("Destination File Path " + destinationFilePath);
                    using (var fileStream = new FileStream(destinationFilePath, FileMode.Create))
                    {
                        SectionFile.CopyTo(fileStream);
                    }
                    fileNameFromAzure = _fileStorageService.UploadFile(destinationFilePath, Input.CourseCodeSelected);
                    System.IO.File.Delete(destinationFilePath);
                }
                CourseSection section = new CourseSection()
                {
                    CourseCode = Input.CourseCodeSelected,
                    CourseContentPath = fileNameFromAzure,
                    SectionTitle = Input.Title,
                    SectionDescription = Input.Description,
                    SectionDuration = Input.Duration,
                    IsEvaluationMandotry = Input.IsEvaluationMandatory,
                    EvalutationId = Input.EvaluationId,
                };
                _sectionsService.CreateSection(section);
                SuccessMessage = "Course section creation successful.";
                Input = new InputModel();
            }
            catch(Exception ex)
            {
                _fileStorageService.LogException(ex);
                ErrorMessage = "Could not create course section.";
            }
            return RedirectToPage();
        }
    }
}
