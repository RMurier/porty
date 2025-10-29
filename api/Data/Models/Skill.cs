namespace api.Data.Models
{
    public class Skill
    {
        public Guid? Id { get; set; }
        public required string Name { get; set; }
        public string? Certification { get; set; }
        public required Guid RefCategory { get; set; }
        public Category? Category { get; set; }
        public ICollection<CareerSkill>? CareerSkills { get; set; }
        public ICollection<ProjectSkill>? ProjectSkills { get; set; }
    }
}