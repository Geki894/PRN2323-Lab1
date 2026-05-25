using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PRN232.LAB1.Repositories.Models
{
    public class Student_Tam
    {
        [Key]
        public int StudentId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }

        public ICollection<Enrollment_Tam> Enrollments { get; set; } = new List<Enrollment_Tam>();
    }
}
