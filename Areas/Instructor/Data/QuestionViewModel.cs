using System.ComponentModel.DataAnnotations;

namespace iLearn.Areas.Instructor.Data
{
    public class QuestionViewModel
    {
        [Required]
        public string Question { get; set; }
        [Required]  
        public string CorrectAnswer { get; set; }
        [Required]
        public List<string> Options { get; set; }
    }
}
