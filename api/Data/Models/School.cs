namespace api.Data.Models
{
    public class School
    {
        public Guid? Id { get; set; }
        public required string Name { get; set; }
        public ICollection<Study>? Studies { get; set; }
    }
}