
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using iLearn.Areas.Identity.Data;
using iLearn.iLearnDbModels;
using iLearn.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace FanKart.Areas.Identity.Pages.Account.Manage
{
    public class ProfileModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<ChangePasswordModel> _logger;
        private readonly IUserService _userService;
        public ProfileModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<ChangePasswordModel> logger,
            IUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _userService = userService;
        }
        [BindProperty]
        public InputModel Input { get; set; }
        [TempData]
        public string StatusMessage { get; set; }
        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            [MaxLength(100)]
            public string Email { get; set; }
            [Required]
            [DataType(DataType.PhoneNumber)]
            [Display(Name = "USA Phone Number")]
            public string PhoneNumber { get; set; }
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "First Name")]
            [MaxLength(20)]
            public string FirstName { get; set; }
            [Required]
            [DataType(DataType.Text)]
            [MaxLength(20)]
            [Display(Name = "Last   Name")]
            public string LastName { get; set; }
            [Required]
            [DataType(DataType.Text)]
            [MaxLength(200)]
            [Display(Name = "Expereince")]
            public string Experience { get; set; }
            [Required]
            [DataType(DataType.Text)]
            [MaxLength(500)]
            [Display(Name = "Certifications")]
            public string Certifications { get; set; }
            [Required]
            [DataType(DataType.Text)]
            [MaxLength(8000)]
            [Display(Name = "Description")]
            public string Description { get; set; }
            public int InstructorId { get; set; }
            public string UserId { get; set; }

        }

        public async void OnGetAsync()
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            Instructor instructor = _userService.GetInstructor(user.Id);
            Input = new InputModel()
            {
                Certifications = instructor.InstructorCertifications,
                Description = instructor.InstructorDescription,
                Email = instructor.InstructorEmailId,
                PhoneNumber = instructor.InstructorPhoneNumber,
                FirstName = instructor.InstructorFirstName,
                LastName = instructor.InstructorLastName,
                Experience = instructor.InstructorExprerience,
                InstructorId = instructor.InstructorId,
                UserId = user.Id
            };
        }

        public void OnPostAsync()
        {
            Instructor instructor = new Instructor()
            {
                InstructorCertifications = Input.Certifications,
                InstructorDescription = Input.Description,
                InstructorEmailId = Input.Email,
                InstructorPhoneNumber = Input.PhoneNumber,
                InstructorFirstName = Input.FirstName,
                InstructorLastName = Input.LastName,
                InstructorExprerience = Input.Experience,
                ModifiedOn = DateTime.Now,
                InstructorId = Input.InstructorId,
                UserId  = Input.UserId
            };
            _userService.UpdateInstructor(instructor);
        }
    }
}
