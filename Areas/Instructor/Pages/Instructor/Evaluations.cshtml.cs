using iLearn.Areas.Instructor.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace iLearn.Views.Instructor
{

    public class EvaluationsModel : PageModel
    {
        [BindProperty]
        public EvaluationFormModel FormData { get; set; }
        public void OnGet()
        {
            // Initialize FormData if needed
            FormData = new EvaluationFormModel
            {
                Questions = new List<QuestionViewModel>()
            };
        }
        public void OnPost()
        {
            
            foreach(var item in FormData.Questions)
            {

            }
        }

        public void OnGetSubmitForm()
        {
            // Initialize FormData if needed
            FormData = new EvaluationFormModel
            {
                Questions = new List<QuestionViewModel>()
            };
        }
    }
}
