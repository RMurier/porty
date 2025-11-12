using System.ComponentModel.DataAnnotations.Schema;

namespace api.Data.Models
{
    public class User
    {
        public Guid? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }  
        public string Password { get; set; }
        public string Salt { get; set; }
        public Guid RefRole { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool? IsEmailValidated { get; set; } = false;
        public Guid? TokenAccountCreated { get; set; } = null;
        public Role? Role { get; set; } = null!;
        [NotMapped]
        public string? AccessToken { get; set; }
        [NotMapped]
        public string? RefreshToken { get; set; }
    }
}
