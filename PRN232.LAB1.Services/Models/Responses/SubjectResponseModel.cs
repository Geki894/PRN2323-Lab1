namespace PRN232.LAB1.Services.Models.Responses
{
    public class SubjectResponseModel
    {
        public int SubjectId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Credits { get; set; }
    }
}
