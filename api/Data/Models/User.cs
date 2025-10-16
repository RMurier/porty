namespace api.Data.Models
{
    public class User
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }  
        public string Password { get; set; }
        public string Pepper { get; set; }
        public string RefRole { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid? TokenAccountCreated { get; set; } = null;
        public Role Role { get; set; } = null!;
    }
}
