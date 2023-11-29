using System.Drawing.Drawing2D;
using System.Drawing;
using iLearn.iLearnDbModels;

namespace iLearn.Models
{
    public class HomeViewModel
    {
        public List<Instructor> Instructors { get; set; } = new List<Instructor>();
        public List<Tuple<bool, string>> AssessmentTypes { get; set; } = new List<Tuple<bool, string>>()
        {
            new Tuple<bool, string>(false,"No"),
            new Tuple<bool, string>(true,"Yes")
        };
        public List<Tuple<bool, string>> CertificationTypes { get; set; } = new List<Tuple<bool, string>>()
        {
            new Tuple<bool, string>(false,"No"),
            new Tuple<bool, string>(true,"Yes")
        };

        //public List<Inventory> Cards { get; set; }  = new List<Inventory> { };
        public List<CardViewModel> Cards { get; set; } = new List<CardViewModel>();
    }
}
