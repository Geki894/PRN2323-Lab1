namespace PRN232.LAB1.Services.Models.Responses
{
    public class CourseResponseModel
    {
        public int CourseId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int SemesterId { get; set; }
        public int SubjectId { get; set; }
        public int MaxStudents { get; set; }
    }
}
