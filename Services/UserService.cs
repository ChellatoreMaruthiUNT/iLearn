using Humanizer;
using iLearn.iLearnDbModels;
using iLearn.Services.Interfaces;
using iLearn.Utils;
using iLearn.Views.Instructor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace iLearn.Services
{
    public class UserService : IUserService
    {
        private readonly iLearnAppContext _context;
        private IConfiguration _config;

        public UserService(iLearnAppContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        public Instructor GetInstructor(string userId)
        {
            return _context.Instructors.FirstOrDefault(instructor => instructor.UserId == userId);    
        }
        public List<Instructor> GetInstructors()
        {
            return _context.Instructors.ToList();
        }
        public Instructor CreateInstructor(Instructor instructor)
        {
            //_context.Instructors.Remove(instructor.UserId);
            _context.Instructors.Add(instructor);
            _context.SaveChanges();
            return instructor;
        }
        public void UpdateInstructor(Instructor instructor)
        {
            _context.Instructors.Update(instructor);
            _context.SaveChanges();
        }
        public UserCourseMapping GetCourseProgress(string emailId, string courseCode)
        {
            var userCourse =  _context.UserCourseMappings.Include(user => user.CourseCodeNavigation).ThenInclude(course => course.CourseSections)
                .Include(user=>user.CourseCodeNavigation).ThenInclude(course => course.Instructor).
                FirstOrDefault(user => user.UserId == emailId && user.CourseCode == courseCode);
            return userCourse;
        }

        public void EnrollCourse(string courseCode, string emailId)
        {
            var firstSectionId = _context.CourseSections.Where(section => section.CourseCode == courseCode).OrderBy(sec => sec.SectionId).FirstOrDefault().SectionId;
            UserCourseMapping userCourseMapping = new UserCourseMapping()
            {
                CourseCode = courseCode,
                UserId = emailId,
                CurrentSectionId = firstSectionId,
                EnrolledOn = DateTime.Now,
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now
            };
            _context.UserCourseMappings.Add(userCourseMapping);
            _context.SaveChanges();
        }

        public void AddNotification(string courseCode, string emailId)
        {
            Notifications notification = new Notifications
            {
                CourseCode = courseCode,
                IsActive = true,
                UserId = emailId,
            };
            _context.Notifications.Add(notification);
            var userMapping = _context.UserCourseMappings.FirstOrDefault(courseMapping => courseMapping.CourseCode == courseCode && courseMapping.UserId == emailId);
            if (userMapping != null)
            {
                userMapping.IsReminderTurndedOn = true;
                _context.UserCourseMappings.Update(userMapping);
            }
            _context.SaveChanges();
        }
        public void RemoveNotification(string courseCode, string emailId)
        {
            var notification = _context.Notifications.Where(notification  => notification.CourseCode == courseCode && notification.UserId == emailId && notification.IsActive == true).FirstOrDefault();
            if(notification != null)
            {
                notification.IsActive = false;
                _context.Notifications.Update(notification);
                var userMapping = _context.UserCourseMappings.FirstOrDefault(courseMapping => courseMapping.CourseCode == courseCode && courseMapping.UserId == emailId);
                if (userMapping != null)
                {
                    userMapping.IsReminderTurndedOn = false;
                    _context.UserCourseMappings.Update(userMapping);
                }
                _context.SaveChanges();
            }
           
        }
    }
}
