using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace iLearn.Areas.Instructor.Data
{
    public class SectionsViewModel
    {
        public int SectionId { get; set; }
        public string SectionTitle { get; set; }
        public string SectionDesscription { get; set; }
        public string CourseCode { get; set; }
    }
}
