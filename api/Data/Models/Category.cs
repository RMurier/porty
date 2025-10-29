namespace api.Data.Models
{
    public class Category
    {
        public Guid? Id { get; set; }
        public required string Name { get; set; }
        public ICollection<Skill>? Skills { get; set; }
    }
}