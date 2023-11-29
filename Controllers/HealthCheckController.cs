using iLearn.Data;
using iLearn.HealthChecks;
using iLearn.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace iLearn.Controllers
{
    //[Route("health")]
    public class HealthCheckController : Controller
    {
        private readonly iLearnContext _context;
        private readonly IFileStorageService _fileStorageService;
        public HealthCheckController(iLearnContext context, IFileStorageService fileStorageService)
        {
            _context = context;
            _fileStorageService = fileStorageService;
        }
        public IActionResult Index()
        {
            //var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot","Health", "index.html");
            //var content = System.IO.File.ReadAllText(filePath);
            //return Content(content, "text/html");
            return View();
        }
        [Route("health")]
        [HttpGet]
        public IActionResult GetHealthStatus()
        {
            // Execute your health checks and get their status
            // Here, assume you have access to instances of your health checks
            var databaseHealthCheck = new ApplicationHealthCheck.DatabaseHealthCheck(_context);
            var fileStorageHealthCheck = new ApplicationHealthCheck.FileStorageHealthCheck(_fileStorageService);

            var databaseStatus = databaseHealthCheck.CheckHealthAsync(null);
            var fileStorageStatus = fileStorageHealthCheck.CheckHealthAsync(null);

            // Construct the response in JSON format
            var healthStatus = new
            {
                database = databaseStatus.Result.Status.ToString(),
                file_storage = fileStorageStatus.Result.Status.ToString()
            };

            return Ok(healthStatus);
        }
    }
}
