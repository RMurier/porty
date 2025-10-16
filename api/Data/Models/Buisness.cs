namespace api.Data.Models
{
    public class Buisness
    {
        public Guid? Id { get; set; }
        public required string Name { get; set; }
        public ICollection<Career>? Careers { get; set; }
    }
}