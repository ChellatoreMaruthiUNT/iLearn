using iLearn.iLearnDbModels;
using Microsoft.EntityFrameworkCore;

namespace iLearn.Services.Interfaces
{
    public interface IUserService
    {
        public Instructor GetInstructor(string userId);
        public Instructor CreateInstructor(Instructor instructor);
        public void UpdateInstructor(Instructor instructor);
        public List<Instructor> GetInstructors();
        public UserCourseMapping GetCourseProgress(string emailId, string courseCode);
        public void EnrollCourse(string courseCode, string emailId);
        public void AddNotification(string courseCode, string emailId);
        public void RemoveNotification(string courseCode, string emailId);

    }
}
