using System;

namespace PRN232.LAB1.Services.Models.Responses
{
    public class SemesterResponseModel
    {
        public int SemesterId { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
