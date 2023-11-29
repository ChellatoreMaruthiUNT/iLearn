using iLearn.iLearnDbModels;
using Microsoft.EntityFrameworkCore;

namespace iLearn.Services.Interfaces
{
    public interface IEvaluationService
    {
        public int CreateEvaluation(Evaluation evaluation);
        public int CreateQuestions(List<Question> question, int evaluationId);
    }
}
