using System;
using System.Collections.Generic;

namespace iLearn.iLearnDbModels
{
    public partial class Course
    {
        public Course()
        {
            CourseSections = new HashSet<CourseSection>();
            UserCourseMappings = new HashSet<UserCourseMapping>();
        }

        public string CourseCode { get; set; } = null!;
        public string? CourseTitle { get; set; }
        public string? CourseDescription { get; set; }
        public int? InstructorId { get; set; }
        public int? Duration { get; set; }
        public bool? IsCertificationProvided { get; set; }
        public bool? IsEvaluationRequired { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual Instructor? Instructor { get; set; }
        public virtual ICollection<CourseSection> CourseSections { get; set; }
        public virtual ICollection<UserCourseMapping> UserCourseMappings { get; set; }
    }
}
