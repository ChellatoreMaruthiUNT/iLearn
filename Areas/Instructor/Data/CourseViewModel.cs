﻿namespace iLearn.Areas.Instructor.Data
{
    public class CourseViewModel
    {
        public string CourseCode { get; set; }
        public string CourseTitle { get; set; }
        public string CourseDescription { get; set; }
        public int Duration { get; set; }
        public iLearn.iLearnDbModels.Instructor? Instructor { get; set; }
    }
}
