using System;
using System.Collections.Generic;

namespace iLearn.iLearnDbModels
{
    public partial class Evaluation
    {
        public Evaluation()
        {
            CourseSections = new HashSet<CourseSection>();
            EvaluationQuestionMappings = new HashSet<EvaluationQuestionMapping>();
        }

        public int EvaluationId { get; set; }
        public string? EvaluationDesc { get; set; }

        public virtual ICollection<CourseSection> CourseSections { get; set; }
        public virtual ICollection<EvaluationQuestionMapping> EvaluationQuestionMappings { get; set; }
    }
}
