namespace api.Data.Models
{
    public class Project
    {
        public Guid? Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public string? Url { get; set; }
        public ICollection<ProjectSkill>? ProjectSkills { get; set; }
    }
}