using System;
using System.Collections.Generic;

namespace iLearn.iLearnDbModels
{
    public partial class CourseSection
    {
        public CourseSection()
        {
            UserCourseMappings = new HashSet<UserCourseMapping>();
        }

        public int SectionId { get; set; }
        public string? CourseCode { get; set; }
        public string? SectionTitle { get; set; }
        public string? SectionDescription { get; set; }
        public int? SectionDuration { get; set; }
        public string? CourseContentPath { get; set; }
        public bool? IsEvaluationMandotry { get; set; }
        public int? EvalutationId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual Course? CourseCodeNavigation { get; set; }
        public virtual Evaluation? Evalutation { get; set; }
        public virtual ICollection<UserCourseMapping> UserCourseMappings { get; set; }
    }
}
