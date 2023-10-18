using iLearn.iLearnDbModels;
using iLearn.Services.Interfaces;
using iLearn.Utils;

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
            return _context.Courses.ToList();
        }
        public Course? GetCourseByCourseCode(string courseCode)
        {
            return _context.Courses.FirstOrDefault(course => course.CourseCode == courseCode);
        }

        public List<Course> GetCoursesByIntructor(int instructorId)
        {
            return _context.Courses.Where(course => course.InstructorId == instructorId).ToList();
        }
    }
}
