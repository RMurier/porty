namespace api.Interfaces
{
    public interface IAuth
    {
        public Task<string> GenerateRefreshToken(Guid userId);
        public Task<string> RefreshAccessToken(string refreshToken, Guid userId);
        public Task<string> GenerateToken(Guid userId);
    }
}
