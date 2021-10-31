using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Api.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace Api.Services
{
    public class SecurityService : ISecurityService
    {
        public bool VerifyPassword(string userName, string password)
        {
            string validator = "HakunaMatata";
            if (userName == "admin" && password == "pass")
                return true;

            if (password != validator)
                return false;

            return true;
        }

        public string GenerateToken(string userName)
        {
            // todo Move to secrets (along with other secret stuff).
            string secret = "Abcd1234()Abcd123[]";
            byte[] encoding = Encoding.ASCII.GetBytes(secret);
            SymmetricSecurityKey key = new SymmetricSecurityKey(encoding);
            string issuer = "https://idp.com";
            string audience = "https://api.com";

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