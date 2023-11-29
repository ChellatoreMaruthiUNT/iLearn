namespace iLearn.iLearnDbModels
{
    public partial class Notifications
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public bool? IsActive    { get; set; }
        public string? CourseCode { get; set; }
    }
}
