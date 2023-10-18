using System;
using System.Collections.Generic;

namespace iLearn.iLearnDbModels
{
    public partial class UserCourseMapping
    {
        public int UserCourseMappingId { get; set; }
        public string? UserId { get; set; }
        public string? CourseCode { get; set; }
        public DateTime? EnrolledOn { get; set; }
        public int? CurrentSectionId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual Course? CourseCodeNavigation { get; set; }
        public virtual CourseSection? CurrentSection { get; set; }
    }
}
