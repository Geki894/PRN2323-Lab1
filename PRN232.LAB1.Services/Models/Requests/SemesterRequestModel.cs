using System;

namespace PRN232.LAB1.Services.Models.Requests
{
    public class SemesterRequestModel
    {
        public string Name { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
