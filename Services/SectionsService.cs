using iLearn.iLearnDbModels;
using iLearn.Services.Interfaces;
using iLearn.Utils;
using static System.Collections.Specialized.BitVector32;

namespace iLearn.Services
{
    public class SectionsService : ISectionsService
    {
        private readonly iLearnAppContext _context;
        private IConfiguration _config;

        public SectionsService(iLearnAppContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public void CreateSection(CourseSection section)
        {
            _context.CourseSections.Add(section);
            _context.SaveChanges();
        }

        public List<CourseSection> GetSections(string courseCode)
        {
            return _context.CourseSections.Where(section => section.CourseCode == courseCode).ToList();    
        }

        public void EditSection(CourseSection section)
        {
            _context.CourseSections.Update(section);
            _context.SaveChanges();
        }
        public CourseSection GetSectionBySectionId(int sectionId)
        {
            return _context.CourseSections.FirstOrDefault(section => section.SectionId == sectionId);
        }

        public void DeleteSection(CourseSection section) 
        {
            _context.CourseSections.Remove(section);
            _context.SaveChanges();
        }

    }
}
