namespace api.Data.Models
{
    public class Career
    {
        public Guid? Id { get; set; }
        public required DateTime StartDate { get; set; } 
        public DateTime? EndDate { get; set; }
        public required string Title { get; set; }
        public string? Comments { get; set; }
        public required Guid RefBuisness { get; set; }
        public Buisness? Buisness { get; set; }
        public ICollection<CareerSkill>? CareerSkills { get; set; }
    }
}