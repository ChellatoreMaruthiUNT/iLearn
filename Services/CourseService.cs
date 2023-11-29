using iLearn.iLearnDbModels;
using iLearn.Services.Interfaces;
using iLearn.Utils;
using Microsoft.EntityFrameworkCore;

namespace iLearn.Services
{
    public class CourseService : ICourseService
    {
        private readonly iLearnAppContext _context;
        private IConfiguration _config;

        public CourseService(iLearnAppContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        public Course CreateCourse(Course course)
        {
            // coursecode is 6 digit unique
            var courseCode = GenericUtils.GenerateRandomString(6);
            course.CourseCode = courseCode;
            _context.Courses.Add(course);
            _context.SaveChanges();
            return course;  
        }
        public Course UpdateCourse(Course course)
        {
            _context.Courses.Update(course);
            _context.SaveChanges();
            return course;
        }
        public List<Course> FilterCourses(string search)
        {
            return _context.Courses.Where(course => course.CourseCode.Contains(search) || course.CourseTitle.Contains(search) || course.CourseDescription.Contains(search)).ToList();
        }
        public List<Course> GetAllCourses()
        {
            return _context.Courses.Include(course => course.Instructor).ToList();
        }
        public Course? GetCourseByCourseCode(string courseCode)
        {
            return _context.Courses.FirstOrDefault(course => course.CourseCode == courseCode);
        }

        public List<Course> GetCoursesByIntructor(int instructorId)
        {
            return _context.Courses.Where(course => course.InstructorId == instructorId).ToList();
        }

        public List<UserCourseMapping> GetCoursesRegisteredByUser(string emailId)
        {
            return _context.UserCourseMappings.Include(user => user.CourseCodeNavigation)
                .ThenInclude(user => user.CourseSections).
                Include(user => user.CourseCodeNavigation).ThenInclude(inst => inst.Instructor)
                .Where(user => user.UserId == emailId).ToList();
        }
        public List<Course> GetCourseItems(
        string searchText = null,
        int? instructorId = null,
        bool? hasAssessment = null,
        bool? hasCertification = null)
        {
            IQueryable<Course> query = _context.Courses.Include(i => i.Instructor);

            if (!string.IsNullOrEmpty(searchText))
            {
                query = query.Where(i => i.CourseTitle.Contains(searchText) || i.CourseDescription.Contains(searchText)
                || i.CourseCode.Contains(searchText) || i.Instructor.InstructorFirstName.Contains(searchText) 
                || i.Instructor.InstructorLastName.Contains(searchText));
            }

            if (instructorId.HasValue)
            {
                query = query.Where(i => i.Instructor.InstructorId == instructorId);
            }

            if (hasAssessment.HasValue)
            {
                query = query.Where(i => i.IsEvaluationRequired == hasAssessment);
            }

            if (hasCertification.HasValue)
            {
                query = query.Where(i => i.IsCertificationProvided == hasCertification);
            }

            return query.ToList();
        }

        public Course GetCourseOverView(string courseCode)
        {
            var course = _context.Courses.Include(course => course.CourseSections).Include(course => course.Instructor).
                FirstOrDefault(course => course.CourseCode == courseCode);
            return course;
        }

        public void MarkCourseComplete(string courseCode, string emailId)
        {
            var userCourseMapping = _context.UserCourseMappings.FirstOrDefault(user => user.CourseCode == courseCode && user.UserId == emailId);
            if (userCourseMapping != null)
            {
                userCourseMapping.IsCourseComplete = true;
                _context.SaveChanges();
            }
        }
    }
}
