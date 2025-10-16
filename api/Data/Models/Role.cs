namespace api.Data.Models
{
    public class Role
    {
        public string Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
