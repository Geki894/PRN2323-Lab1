using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRN232.LAB1.Repositories.Models
{
    public class Course_Tam
    {
        [Key]
        public int CourseId { get; set; }
        public string Name { get; set; } = string.Empty;

        [ForeignKey("Semester_Tam")]
        public int SemesterId { get; set; }
        public Semester_Tam? Semester { get; set; }

        [ForeignKey("Subject_Tam")]
        public int SubjectId { get; set; }
        public Subject_Tam? Subject { get; set; }

        public int MaxStudents { get; set; }

        public ICollection<Enrollment_Tam> Enrollments { get; set; } = new List<Enrollment_Tam>();
    }
}
