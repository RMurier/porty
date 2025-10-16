namespace api.Data.Models
{
    public class ProjectSkill
    {
        public Guid? Id { get; set; }
        public required Guid RefProject { get; set; }
        public Project? Project { get; set; }
        public required Guid RefSkill { get; set; }
        public Skill? Skill { get; set; }
    }
}