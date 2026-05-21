using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRN232.LAB1.Repositories.Models
{
    public class Enrollment_Tam
    {
        [Key]
        public int EnrollmentId { get; set; }

        [ForeignKey("Student_Tam")]
        public int StudentId { get; set; }
        public Student_Tam? Student { get; set; }

        [ForeignKey("Course_Tam")]
        public int CourseId { get; set; }
        public Course_Tam? Course { get; set; }

        public DateTime EnrollDate { get; set; }
        public string Status { get; set; } = "Active"; // e.g., Active, Completed, Dropped
    }
}
