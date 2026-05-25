namespace PRN232.LAB1.Services.Models.Requests
{
    public class CourseRequestModel
    {
        public string Name { get; set; } = string.Empty;
        public int SemesterId { get; set; }
        public int SubjectId { get; set; }
        public int MaxStudents { get; set; }
    }
}
