using System;

namespace PRN232.LAB1.Services.Models.Requests
{
    public class EnrollmentRequestModel
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public DateTime EnrollDate { get; set; }
        public string Status { get; set; } = "Active";
    }
}
