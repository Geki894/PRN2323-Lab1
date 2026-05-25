using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PRN232.LAB1.Repositories.Models
{
    public class Subject_Tam
    {
        [Key]
        public int SubjectId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Credits { get; set; }

        public ICollection<Course_Tam> Courses { get; set; } = new List<Course_Tam>();
    }
}
