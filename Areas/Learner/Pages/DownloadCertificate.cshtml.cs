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
    public class DownlaodCertificateModel : PageModel
    {
        private readonly ICourseService _courseService;
        private readonly UserManager<ApplicationUser> _userManager;

        public DownlaodCertificateModel(ICourseService courseService, UserManager<ApplicationUser> userManager)
        {
            _courseService = courseService;
            _userManager = userManager;
        }
        [BindProperty]
        public Course Course { get; set; }
        [BindProperty]
        public string UserName { get; set; }

        public async Task OnGetAsync(string courseCode)
        {
            _courseService.MarkCourseComplete(courseCode, User.Identity.Name);
            Course = _courseService.GetCourseByCourseCode(courseCode);
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            UserName = user.LastName + " " + user.FirstName;
        }

        public void OnPost()
        {      
            
        }
    }
}
