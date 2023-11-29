using iLearn.Areas.Instructor.Data;
using iLearn.iLearnDbModels;
using iLearn.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;

namespace iLearn.Controllers
{
    public class InstructorController : Controller
    {
        private readonly IEvaluationService _evaluationService;
        public InstructorController(IEvaluationService evaluationService)
        {     
            _evaluationService = evaluationService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PostCourseSections([FromBody] EvaluationFormModel FormData)
        {
           // First create Evaluation
           Evaluation evaluation = new Evaluation()
           {
               EvaluationDesc = "Y"
           };
            int evaluationId = _evaluationService.CreateEvaluation(evaluation);
            List<Question> questions = new List<Question>();
            foreach (var question in FormData.Questions)
            {
                List<QuestionOptionsMapping> options = new List<QuestionOptionsMapping>();
                Int16 optionId = 1;
                foreach (var option in question.Options)
                {                    
                    options.Add(new QuestionOptionsMapping()
                    {
                        CreatdOn = DateTime.Now,
                        ModifiedOn = DateTime.Now,
                        OptionId = optionId,
                        OptionDesc = option ?? string.Empty
                    });
                    optionId++;
                }
                questions.Add(new Question()
                {
                    CreatedOn = DateTime.Now,
                    QuestionDesc = question.Question,
                    QuestionAnswerOptionId = Convert.ToInt16(question.CorrectAnswer),
                    QuestionOptionsMappings = options
                });
            }
            _evaluationService.CreateQuestions(questions, evaluationId);
            return Json(evaluationId);
        }
    }
}
