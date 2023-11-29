using Aspose.Pdf.Text;
using Aspose.Words;
using iLearn.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using iLearn.Areas.Identity.Data;

namespace iLearn.Controllers
{
    public class PdfController : Controller
    {
        private readonly IFileStorageService _fileStorageService;
        private readonly ISectionsService _sectionsService;
        private readonly ICourseService _courseService;
        private readonly UserManager<ApplicationUser> _userManager;

        public PdfController(IFileStorageService fileStorageService, ISectionsService sectionsService, ICourseService courseService
            , UserManager<ApplicationUser> userManager)
        {
            _fileStorageService = fileStorageService;
            _sectionsService = sectionsService;
            _courseService = courseService;
            _userManager = userManager;
        }
        public IActionResult GetPdf(int sectionId)
        {
            // Replace this with code to load the PDF byte array from your source.
            var section = _sectionsService.GetSectionBySectionId(sectionId);
            byte[] fileBytes = _fileStorageService.DownloadFile(section.CourseContentPath, section.CourseCode);

            if (fileBytes != null)
            {
                if(section.CourseContentPath.EndsWith("docx"))
                {
                    byte[] pdfBytes = ConvertToPdf(fileBytes);
                    return File(pdfBytes, "application/pdf");
                }
                return File(fileBytes, "application/pdf");
            }

            return NotFound();
        }

        private byte[] ConvertToPdf(byte[] docxBytes)
        {
            using (MemoryStream docxStream = new MemoryStream(docxBytes))
            {
                using (MemoryStream pdfStream = new MemoryStream())
                {
                    Aspose.Words.Document doc = new Aspose.Words.Document(docxStream);
                    doc.Save(pdfStream, Aspose.Words.SaveFormat.Pdf);
                    return pdfStream.ToArray();
                }
            }
        }

        //[HttpGet]
        //public async Task<IActionResult> GetCertificate(string courseCode)
        //{
        //    var user = await _userManager.FindByEmailAsync(User.Identity.Name);
        //    var course = _courseService.GetCourseByCourseCode(courseCode);
        //    var certificateFileBytes = _fileStorageService.GetCertificate();
        //    Dictionary<string, string> replacements = new Dictionary<string, string>();
        //    replacements.Add("@EMANESRUOC", course.CourseTitle);
        //    replacements.Add("@EMANRESU", user.);
        //    var fileBytes = ReplaceTextInPdf(certificateFileBytes, replacements);
        //    return File(fileBytes, "application/pdf");
        //}

        [HttpGet]
        public async Task<ContentResult> GetCertificates(string courseCode)
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            var course = _courseService.GetCourseByCourseCode(courseCode);
            var certificateFileBytes = _fileStorageService.GetCertificate();
            Dictionary<string, string> replacements = new Dictionary<string, string>();
            replacements.Add("@EMANESRUOC", course.CourseTitle);
            replacements.Add("@EMANRESU", user.LastName + " " + user.FirstName);
            var fileBytes = ReplaceTextInPdf(certificateFileBytes, replacements);
            string base64 = Convert.ToBase64String(fileBytes, 0, fileBytes.Length);

            return Content(base64);
        }
        public static byte[] ReplaceTextInPdf(byte[] pdfBytes, Dictionary<string, string> replacements)
        {
            try
            {
                using (MemoryStream inStream = new MemoryStream(pdfBytes))
                using (Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document(inStream))
                {
                    foreach (var replacement in replacements)
                    {
                        string searchText = replacement.Key;
                        string replacementText = replacement.Value;

                        TextFragmentAbsorber textFragmentAbsorber = new TextFragmentAbsorber(searchText);
                        pdfDocument.Pages.Accept(textFragmentAbsorber);

                        foreach (TextFragment textFragment in textFragmentAbsorber.TextFragments)
                        {
                            textFragment.Text = replacementText;
                        }
                    }

                    using (MemoryStream outStream = new MemoryStream())
                    {
                        pdfDocument.Save(outStream);
                        return outStream.ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }
    }
}
