using iLearn.iLearnDbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace iLearn.Services.Interfaces
{
    public interface ISectionsService
    {
        public void CreateSection(CourseSection section);
        public List<CourseSection> GetSections(string courseCode);
        public void EditSection(CourseSection section, int sectionDurationBeforeEdit);
        public CourseSection GetSectionBySectionId(int sectionId);
        public CourseSection ProceedToSection(int nextSectionId, string emailId);
        public void DeleteSection(CourseSection section);
        public Tuple<int, int> GetPreviousAndNextSectionid(int currentSectionId);
    }
}
