using System;

namespace PRN232.LAB1.Services.Models.Responses
{
    public class EnrollmentResponseModel
    {
        public int EnrollmentId { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public DateTime EnrollDate { get; set; }
        public string Status { get; set; } = "Active";
    }
}
