namespace api.Data.Models
{
    public class Study
    {
        public Guid? Id { get; set; }
        public required DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public required string Title { get; set; }
        public string? Comments { get; set; }
        public required Guid RefSchool { get; set; }
        public School? School { get; set; }
    }
}