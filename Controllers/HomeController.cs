using Hangfire;
using iLearn.iLearnDbModels;
using iLearn.Models;
using iLearn.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace iLearn.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICourseService _courseService;
        private readonly IUserService _userService;
        private readonly IJobsService _jobsService;
        private IConfiguration _config;
        public HomeController(ILogger<HomeController> logger, ICourseService courseService, IUserService userService, IJobsService jobsService, IConfiguration config)
        {
            _courseService = courseService;
            _logger = logger;
            _userService = userService;
            _jobsService = jobsService;
            _config = config;
        }

        public IActionResult Index()
        {
            HomeViewModel viewModel = new HomeViewModel();
            viewModel.Instructors = _userService.GetInstructors();
            viewModel.Cards = CreateCardsFromInventory(_courseService.GetCourseItems());
            //RecurringJob.AddOrUpdate(() => _jobsService.SendNotifications(), Cron.MinuteInterval(_config.GetValue<int>("HangfireJobs:RunNotificationsJobIntervalInMinutes")));

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Search(string searchText)
        {
            List<Course> courses = _courseService.GetCourseItems(searchText);
            return PartialView("_Card", CreateCardsFromInventory(courses));
        }

        [HttpGet]
        public IActionResult Filter(int? instructorId, bool? hasAssessment, bool? hasCertification)
        {
            List<Course> courses = _courseService.GetCourseItems(null, instructorId, hasAssessment, hasCertification);
            return PartialView("_Card", CreateCardsFromInventory(courses));
        }

        [HttpGet]
        public IActionResult Suggestions(string term)
        {
            List<Course> courses = _courseService.GetCourseItems(term);
            var results = courses
                .Select(i => new { label = i.CourseTitle, value = i.CourseTitle })
                .ToList();

            return Json(results.Distinct());
        }

        public List<CardViewModel> CreateCardsFromInventory(List<Course> courses)
        {
            List<CardViewModel> uniqueCourses = new List<CardViewModel>();
            List<UserCourseMapping> userCourses = _courseService.GetCoursesRegisteredByUser(User.Identity.Name);
            foreach (var item in courses.ToList())
            {

                if (!uniqueCourses.Any(inven => inven.CourseCode == item.CourseCode))
                {
                    bool isEnrolled = userCourses.Where(cour => cour.CourseCode == item.CourseCode).Any();
                    CardViewModel cardViewModel = new CardViewModel()
                    {
                        CourseCode = item.CourseCode,
                        CourseTitle = item.CourseTitle,
                        CourseDescripton = item.CourseDescription,
                        Duration = item.Duration ?? 0,
                        HasAssessment = item.IsEvaluationRequired,
                        HasCertification = item.IsCertificationProvided,
                        Instructor = item.Instructor,
                        InstructorId = item.InstructorId,
                        IsEnrolled = isEnrolled,
                    };
                    uniqueCourses.Add(cardViewModel);
                }
                else
                {
                    var currentInventory = uniqueCourses.FirstOrDefault(inven => inven.CourseCode == item.CourseCode);
                }
            }
            return uniqueCourses;
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}