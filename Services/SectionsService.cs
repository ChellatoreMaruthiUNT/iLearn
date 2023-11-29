using Aspose.Words.Fields;
using iLearn.iLearnDbModels;
using iLearn.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

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
            var course = _context.Courses.FirstOrDefault(course => course.CourseCode == section.CourseCode);
            course.Duration = (course.Duration ?? 0) + section.SectionDuration;
            _context.Courses.Update(course);
            _context.SaveChanges();
        }

        public List<CourseSection> GetSections(string courseCode)
        {
            return _context.CourseSections.Where(section => section.CourseCode == courseCode).ToList();    
        }

        public void EditSection(CourseSection section, int sectionDurationBeforeEdit)
        {

            _context.CourseSections.Update(section);
            
            if (sectionDurationBeforeEdit != 0)
            {
                var course = _context.Courses.FirstOrDefault(c => c.CourseCode == section.CourseCode);
                course.Duration = (course.Duration ?? 0) + sectionDurationBeforeEdit;
                _context.Courses.Update(course);
            }

            _context.SaveChanges();
        }
        public CourseSection GetSectionBySectionId(int sectionId)
        {
            return _context.CourseSections.Include(section => section.Evalutation).ThenInclude(evaluation => evaluation.EvaluationQuestionMappings).ThenInclude(question => question.Question)
                .ThenInclude(question => question.QuestionOptionsMappings).FirstOrDefault(section => section.SectionId == sectionId);
        }
        public Tuple<int, int> GetPreviousAndNextSectionid(int currentSectionId)
        {
            int nextSectionId = 0;
            int previousSectionId = 0;
            var nextSection = _context.CourseSections.Include(section => section.Evalutation)
                .Where(cs => cs.SectionId > currentSectionId)
                .OrderBy(cs => cs.SectionId)
                .FirstOrDefault();
            if(nextSection != null)
            {
                nextSectionId = nextSection.SectionId;
            }
            var prevSection = _context.CourseSections.Include(section => section.Evalutation)
                .Where(cs => cs.SectionId < currentSectionId)
                .OrderByDescending(cs => cs.SectionId)
                .FirstOrDefault();
            if (prevSection != null)
            {
                previousSectionId = prevSection.SectionId;
            }
            return new Tuple<int, int>(previousSectionId, nextSectionId);
        }
        public CourseSection ProceedToSection(int nextSectionId, string emailId)
        {
            var nextSection = _context.CourseSections.Include(section => section.CourseCodeNavigation).Include(section => section.Evalutation).ThenInclude(evaluation => evaluation.EvaluationQuestionMappings)
                .ThenInclude(question => question.Question).ThenInclude(question => question.QuestionOptionsMappings).FirstOrDefault(section => section.SectionId == nextSectionId);
            if(nextSection != null)
            {
                var userCourseMapping = _context.UserCourseMappings.FirstOrDefault(user => user.CourseCode == nextSection.CourseCode && user.UserId == emailId);
                if (userCourseMapping != null && nextSection.SectionId > userCourseMapping.CurrentSectionId)
                {
                    userCourseMapping.CurrentSectionId = nextSection.SectionId;
                    _context.SaveChanges();
                }
            }
            else
            {

            }
            return nextSection;            
        }

        public void MarkCourseComplete(int currentSectionId, string emailId)
        {
            var section = _context.CourseSections.FirstOrDefault(section => section.SectionId == currentSectionId);
            var userCourseMapping = _context.UserCourseMappings.FirstOrDefault(user => user.CourseCode == section.CourseCode && user.UserId == emailId);
            if (userCourseMapping != null)
            {
                userCourseMapping.IsCourseComplete = true;  
                _context.SaveChanges();
            }
        }

        public void DeleteSection(CourseSection section) 
        {
            _context.CourseSections.Remove(section);
            _context.SaveChanges();
        }

    }
}
