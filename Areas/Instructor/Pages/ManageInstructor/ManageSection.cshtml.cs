using Humanizer;
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
    public class ManageSectionModel : PageModel
    {
        private readonly IFileStorageService _fileStorageService;
        private readonly ISectionsService _sectionsService;
        private readonly ICourseService _courseService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ManageSectionModel(IFileStorageService fileStorageService, ISectionsService sectionsService,
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
            [Display(Name = "Select Course")]
            public string CourseCodeSelected { get; set; }
            public string CourseTitle { get; set; }
            
            [Display(Name = "Is Evaluation Mandatory")]
            public bool IsEvaluationMandatory { get; set; }
            public int SectionId { get; set; }
            public string CourseCode { get; set; }
            public int EvaluationId { get; set; }
        }
        public async void OnGet(int sectionId)
        {
            var section = _sectionsService.GetSectionBySectionId(sectionId);
            var course = _courseService.GetCourseByCourseCode(section.CourseCode);
            Input = new InputModel()
            {
                SectionId = sectionId,
                Title = section.SectionTitle,
                Description = section.SectionDescription,
                Duration = section.SectionDuration ?? 0,
                IsEvaluationMandatory = section.IsEvaluationMandotry ?? false,
                CourseCode = section.CourseCode,
                CourseCodeSelected = section.CourseCode,
                CourseTitle = course.CourseTitle,
                EvaluationId = section.EvalutationId ?? 0
            };
        }

        public void OnPost()
        {
            try
            {
                string fileNameFromAzure = string.Empty;
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
                    fileNameFromAzure = _fileStorageService.UploadFile(destinationFilePath, Input.CourseCode);
                    System.IO.File.Delete(destinationFilePath);
                }
                // update section
                CourseSection section = new CourseSection()
                {
                    SectionId = Input.SectionId,
                    SectionTitle = Input.Title,
                    SectionDescription = Input.Description,
                    SectionDuration = Input.Duration,
                    IsEvaluationMandotry = Input.IsEvaluationMandatory,
                    CourseCode=Input.CourseCode,
                    CourseContentPath = fileNameFromAzure,
                    EvalutationId = Input.EvaluationId
                };
                _sectionsService.EditSection(section);
                Input = new InputModel();
                SuccessMessage = "Edit section successful";
            }
            catch(Exception ex)
            {
                ErrorMessage = "Could not edit section. Please try again later.";
            }

        }
    }
}
