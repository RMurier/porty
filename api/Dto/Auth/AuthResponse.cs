namespace api.Dto.Auth
{
    public class AuthResponse
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public bool IsEmailValidated { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
