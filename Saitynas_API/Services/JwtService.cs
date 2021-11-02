using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Saitynas_API.Models;
using Saitynas_API.Models.Authentication;
using Saitynas_API.Models.Entities.User;

namespace Saitynas_API.Services
{
    public interface IJwtService
    {
        public string GenerateSecurityToken(User user);

        public RefreshToken GenerateRefreshToken(User user);
    }

    public class JwtService : IJwtService
    {
        private SymmetricSecurityKey SecurityKey => new(Encoding.ASCII.GetBytes(_secret));

        private readonly string _secret;
        private readonly JwtSettings _settings;

        private const string JwtSecretKey = "JwtSecret";

        public JwtService(IConfiguration config, IOptions<JwtSettings> settings)
        {
            _secret = config[JwtSecretKey] ?? Environment.GetEnvironmentVariable(JwtSecretKey);
            _settings = settings.Value;
        }

        public string GenerateSecurityToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var claims = new ClaimsIdentity(new[]
            {
                new Claim(CustomClaims.TokenID, Guid.NewGuid().ToString()),
                new Claim(CustomClaims.UserID, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.RoleId.ToString())
            });

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.Add(_settings.AccessTokenTTL),
                SigningCredentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public RefreshToken GenerateRefreshToken(User user)
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            byte[] randomBytes = new byte[64];
            rngCryptoServiceProvider.GetBytes(randomBytes);

            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(randomBytes),
                ExpiresAt = DateTime.UtcNow.Add(_settings.RefreshTokenTTL),
                UserId = user.Id
            };

            return refreshToken;
        }
    }
}
