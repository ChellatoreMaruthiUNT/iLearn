using Microsoft.AspNetCore.Identity;

namespace iLearn.Areas.Identity.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CountryCode { get; set; }
        public bool IsInstructor { get; set; }
        public int InstructorId { get; set; }
    }
}
