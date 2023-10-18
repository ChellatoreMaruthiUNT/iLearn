using iLearn.iLearnDbModels;
using Microsoft.EntityFrameworkCore;

namespace iLearn.Services.Interfaces
{
    public interface ICourseService
    {
        public Course CreateCourse(Course course);
        public Course UpdateCourse(Course course);
        public List<Course> FilterCourses(string search);
        public List<Course> GetAllCourses();
        public Course? GetCourseByCourseCode(string courseCode);
        public List<Course> GetCoursesByIntructor(int instructorId);
    }
}
