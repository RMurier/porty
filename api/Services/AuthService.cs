using api.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace api.Services
{
    public class AuthService : IAuth
    {
        private readonly IConfiguration _config;
        public AuthService(IConfiguration config)
        {
            _config = config;
        }
        public async Task<string> GenerateRefreshToken(Guid userId)
        {
            IConfigurationSection jwtConfig = _config.GetSection("Jwt");
            string keyjwt = jwtConfig["Key"] ?? throw new ApplicationException("JWT key is not configured.");
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyjwt));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddDays(int.Parse(jwtConfig["timeValidityRefreshToken"])),
                SigningCredentials = creds,
                Audience = jwtConfig["Audience"],
                Issuer = jwtConfig["Issuer"],
                Claims = new Dictionary<string, object>
                {
                    { "userId", userId.ToString() }
                }
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(securityToken);
        }

        public async Task<string> RefreshAccessToken(string refreshToken, Guid userId)
        {
            IConfigurationSection jwtConfig = _config.GetSection("Jwt");
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            string keyjwt = jwtConfig["Key"] ?? throw new ApplicationException("JWT key is not configured.");
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyjwt));

            try
            {
                ClaimsPrincipal principal = tokenHandler.ValidateToken(refreshToken, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = jwtConfig["Issuer"],
                    ValidAudience = jwtConfig["Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                JwtSecurityToken jwt = (JwtSecurityToken)validatedToken;

                if (!jwt.Payload.TryGetValue("userId", out object? tokenUserId) || tokenUserId.ToString() != userId.ToString())
                {
                    Console.WriteLine($"UserId mismatch or missing. Token UserId: {tokenUserId}, Expected UserId: {userId}");
                    throw new SecurityTokenException("Invalid refresh token.");
                }

                return await GenerateToken(userId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Refresh token validation failed: {ex.Message}");
                throw new SecurityTokenException("Invalid refresh token.", ex);
            }
        }

        public async Task<string> GenerateToken(Guid userId)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString())
            };

            IConfigurationSection jwtConfig = _config.GetSection("Jwt");
            string keyJWT = jwtConfig["Key"] ?? throw new ApplicationException("JWT key is not configured.");
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyJWT));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: jwtConfig["Issuer"],
                audience: jwtConfig["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(jwtConfig["timeValidityAccessToken"])),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
