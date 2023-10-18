using System;
using System.Collections.Generic;

namespace iLearn.iLearnDbModels
{
    public partial class Question
    {
        public Question()
        {
            EvaluationQuestionMappings = new HashSet<EvaluationQuestionMapping>();
            QuestionOptionsMappings = new HashSet<QuestionOptionsMapping>();
        }

        public int QuestionId { get; set; }
        public string? QuestionDesc { get; set; }
        public short? QuestionAnswerOptionId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual ICollection<EvaluationQuestionMapping> EvaluationQuestionMappings { get; set; }
        public virtual ICollection<QuestionOptionsMapping> QuestionOptionsMappings { get; set; }
    }
}
