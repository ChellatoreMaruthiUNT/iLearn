using iLearn.iLearnDbModels;
using System.Drawing;

namespace iLearn.Models
{
    public class CardViewModel
    {
        public string CourseCode { get; set; }
        public string CourseTitle { get; set; }
        public string CourseDescripton { get; set; }
        public int Duration { get; set; }
        public bool? HasAssessment { get; set; }
        public bool? HasCertification { get; set; }
        public int? InstructorId { get; set; }
        public bool IsEnrolled { get; set; }
        public virtual Instructor? Instructor { get; set; }
    }
}
