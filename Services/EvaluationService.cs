using iLearn.iLearnDbModels;
using iLearn.Services.Interfaces;

namespace iLearn.Services
{
    public class EvaluationService : IEvaluationService
    {
        private readonly iLearnAppContext _context;
        private IConfiguration _config;

        public EvaluationService(iLearnAppContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        public int CreateEvaluation(Evaluation evaluation)
        {
            evaluation.EvaluationId = _context.GetNextPrimaryKeyValue<Evaluation>();
            _context.Evaluations.Add(evaluation);
            return evaluation.EvaluationId;
        }

        public int CreateQuestions(List<Question> questions, int evaluationId)
        {
            foreach (var question in questions) 
            {
                // create question
                var questionId = _context.GetNextPrimaryKeyValue<Question>();
                question.QuestionId = questionId;
                _context.Questions.Add(question);
                _context.SaveChanges();
                // create question evaluation map
                var evaluationQuestionMapId = _context.GetNextPrimaryKeyValue<EvaluationQuestionMapping>();
                var evaluationQuestionMap = new EvaluationQuestionMapping()
                {
                    EvaluationQuestionMappingId = evaluationQuestionMapId,
                    CreatdOn = DateTime.Now,
                    QuestionId = questionId,
                    ModifiedOn = DateTime.Now,
                    EvaluationId = evaluationId
                };
                _context.EvaluationQuestionMappings.Add(evaluationQuestionMap);
                _context.SaveChanges();
            }
            return evaluationId;
        }
    }
}
