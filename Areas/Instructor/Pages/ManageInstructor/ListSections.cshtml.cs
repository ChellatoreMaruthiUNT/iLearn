using iLearn.Areas.Identity.Data;
using iLearn.Areas.Instructor.Data;
using iLearn.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace iLearn.Views.Instructor
{
    public class ListSectionsModel : PageModel
    {
        private readonly ISectionsService _sectionService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ListSectionsModel(ISectionsService sectionsService, UserManager<ApplicationUser> userManager)
        {
            _sectionService = sectionsService;
            _userManager = userManager;
        }
        [BindProperty]
        public List<SectionsViewModel> SectionsList { get; set; } = new List<SectionsViewModel>();

        public async void OnGet(string courseCode)
        {
            var sections = _sectionService.GetSections(courseCode);
            if (sections != null)
            {
                foreach (var item in sections)
                {
                    SectionsList.Add(new SectionsViewModel()
                    {
                        CourseCode = item.CourseCode,
                        SectionId = item.SectionId,
                        SectionTitle = item.SectionTitle,
                        SectionDesscription = item.SectionDescription
                    });
                }
            }
        }

        public async void OnPost()
        {         
        }
    }
}
