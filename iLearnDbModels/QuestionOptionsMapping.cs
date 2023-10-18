using System;
using System.Collections.Generic;

namespace iLearn.iLearnDbModels
{
    public partial class QuestionOptionsMapping
    {
        public int QuestionOptionsMapId { get; set; }
        public int? QuestionId { get; set; }
        public short? OptionId { get; set; }
        public string OptionDesc { get; set; }
        public DateTime? CreatdOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual Question? Question { get; set; }
    }
}
