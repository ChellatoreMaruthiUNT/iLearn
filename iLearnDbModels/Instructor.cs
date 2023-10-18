using System;
using System.Collections.Generic;

namespace iLearn.iLearnDbModels
{
    public partial class Instructor
    {
        public Instructor()
        {
            Courses = new HashSet<Course>();
        }

        public int InstructorId { get; set; }
        public string? InstructorFirstName { get; set; }
        public string? InstructorLastName { get; set; }
        public string? InstructorDescription { get; set; }
        public string? InstructorEmailId { get; set; }
        public string? InstructorPhoneNumber { get; set; }
        public string? InstructorExprerience { get; set; }
        public string? InstructorCertifications { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? UserId { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
