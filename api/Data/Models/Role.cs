namespace api.Data.Models
{
    public class Role
    {
        public Guid? Id { get; set; }
        public required string Name { get; set; } = null!;
        public string? Description { get; set; }

        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
