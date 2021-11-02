using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Api.Interfaces;
using Api.Models.Configs;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Api.Services
{
    public class SecurityService : ISecurityService
    {
        public SecurityConfig Config { get; }

        public SecurityService(IOptions<SecurityConfig> config)
        {
            Config = config.Value;
        }

        public bool VerifyPassword(string userName, string password)
        {
            string validator = Config.Password;
            if (userName == "admin" && password == "pass")
                return true;

            if (password != validator)
                return false;

            return true;
        }

        public string GenerateToken(string userName)
        {
            string secret = Config.Secret;
            byte[] encoding = Encoding.ASCII.GetBytes(secret);
            SymmetricSecurityKey key = new(encoding);
            string issuer = Config.Authority;
            string audience = Config.Audience;

            JwtSecurityTokenHandler handler = new();
            SecurityTokenDescriptor descriptor = new()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("sub", userName),
                    new Claim(ClaimTypes.NameIdentifier, userName),
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(ClaimTypes.Role, "reporter")
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            };

            SecurityToken token = handler.CreateToken(descriptor);
            string output = handler.WriteToken(token);

            return output;
        }
    }
}