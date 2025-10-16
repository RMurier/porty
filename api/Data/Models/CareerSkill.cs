namespace api.Data.Models
{
    public class CareerSkill
    {
        public Guid? Id { get; set; }
        public required Guid RefCareer { get; set; }
        public Career? Career { get; set; }
        public required Guid RefSkill { get; set; }
        public Skill? Skill { get; set; }
    }
}