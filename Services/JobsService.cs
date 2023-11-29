using iLearn.iLearnDbModels;
using iLearn.Services.Interfaces;

namespace iLearn.Services
{
    public class JobsService: IJobsService
    {
        private readonly iLearnAppContext _context;
        private readonly IEmailService _emailService;

        public JobsService(iLearnAppContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public void SendNotifications()
        {
            var notifications = _context.Notifications.Where(notification => notification.IsActive == true).ToList();
            foreach(var notification in notifications) 
            {
                var course = _context.Courses.FirstOrDefault(course => course.CourseCode ==  notification.CourseCode);
                _emailService.SendEmail(notification.UserId, "You are doing great!!! Happy learning", "Please resume learning on your enrolled course " + course.CourseTitle);
            }
        }
    }
}
