using System;
using System.Collections.Generic;

namespace iLearn.iLearnDbModels
{
    public partial class EvaluationQuestionMapping
    {
        public int EvaluationQuestionMappingId { get; set; }
        public int? EvaluationId { get; set; }
        public int? QuestionId { get; set; }
        public DateTime? CreatdOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual Evaluation? Evaluation { get; set; }
        public virtual Question? Question { get; set; }
    }
}
