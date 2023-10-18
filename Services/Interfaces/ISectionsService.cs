using iLearn.iLearnDbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace iLearn.Services.Interfaces
{
    public interface ISectionsService
    {
        public void CreateSection(CourseSection section);
        public List<CourseSection> GetSections(string courseCode);
        public void EditSection(CourseSection section);
        public CourseSection GetSectionBySectionId(int sectionId);
        public void DeleteSection(CourseSection section);
    }
}
