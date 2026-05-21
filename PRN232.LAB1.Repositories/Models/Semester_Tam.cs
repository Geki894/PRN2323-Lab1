using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PRN232.LAB1.Repositories.Models
{
    public class Semester_Tam
    {
        [Key]
        public int SemesterId { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public ICollection<Course_Tam> Courses { get; set; } = new List<Course_Tam>();
    }
}
