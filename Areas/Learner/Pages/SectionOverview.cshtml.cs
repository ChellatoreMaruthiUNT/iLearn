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
    public class SectionOverviewModel : PageModel
    {
        private readonly ISectionsService _sectionsService;
        private readonly UserManager<ApplicationUser> _userManager;

        public SectionOverviewModel(ISectionsService sectionsService, UserManager<ApplicationUser> userManager)
        {
            _sectionsService = sectionsService;
            _userManager = userManager;
        }
        [BindProperty]
        public int PreviousSectionId { get; set; }
        [BindProperty]
        public int NextSectionId { get; set; }
        [BindProperty]
        public CourseSection Section { get; set; } 

        public void OnGet(int sectionId)
        {
            Section = _sectionsService.ProceedToSection(sectionId, User.Identity.Name);
            var currentAndPreviousSectionIds = _sectionsService.GetPreviousAndNextSectionid(sectionId);
            PreviousSectionId = currentAndPreviousSectionIds.Item1;
            NextSectionId = currentAndPreviousSectionIds.Item2;
        }

        public void OnPost()
        {      
            
        }
    }
}
