using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Saitynas_API.Models.Authentication;
using Saitynas_API.Models.UserEntity;

namespace Saitynas_API.Services.JwtService
{
    public class JwtService : IJwtService
    {
        private SymmetricSecurityKey SecurityKey => new(Encoding.ASCII.GetBytes(_secret));

        private readonly string _secret;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly JwtSettings _settings;

        public JwtService(IConfiguration config, IOptions<JwtSettings> settings)
        {
            _secret = config["JwtSecret"] ?? Environment.GetEnvironmentVariable("JwtSecret");
            _settings = settings.Value;

            _tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = SecurityKey,
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };
        }

        public string GenerateSecurityToken(JwtUser jwtUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var claims = new ClaimsIdentity(new[]
            {
                new Claim("token_id", Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Email, jwtUser.Email),
                new Claim(ClaimTypes.Role, jwtUser.RoleId.ToString())
            });

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddMinutes(_settings.AccessTokenTTL),
                SigningCredentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public string ValidateToken(string token)
        {
            if (token == null) return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, _tokenValidationParameters, out var validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                string email = jwtToken.Claims.FirstOrDefault(c => c.Type == "email")?.Value;

                return email;
            }
            catch
            {
                return null;
            }
        }

        public RefreshToken GenerateRefreshToken(User user)
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            byte[] randomBytes = new byte[64];
            rngCryptoServiceProvider.GetBytes(randomBytes);

            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(randomBytes),
                ExpiresAt = DateTime.UtcNow.AddDays(_settings.RefreshTokenTTL),
                UserId = user.Id
            };

            return refreshToken;
        }
    }
}
