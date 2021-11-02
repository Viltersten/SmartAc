using System.Linq;
using Api.Interfaces;
using Api.Models.Configs;
using Api.Services;
using Microsoft.Extensions.Options;
using Xunit;

namespace Test
{
    public class SecurityServiceTest
    {
        private ISecurityService Sut { get; }

        private const string PASSWORD = "HakunaMatata";
        private const string SECRET = "ShhhNoSquicking!";

        public SecurityServiceTest()
        {
            SecurityConfig config = new()
            {
                Password = PASSWORD,
                Secret = SECRET
            };

            Sut = new SecurityService(Options.Create(config));
        }

        [Fact]
        public void AdminLogin()
        {
            const string userName = "admin";
            const string password = "pass";

            bool actual = Sut.VerifyPassword(userName, password);

            Assert.True(actual);
        }

        [Fact]
        public void RegularLoginSuccess()
        {
            const string password = PASSWORD;

            bool actual = Sut.VerifyPassword(null, password);

            Assert.True(actual);
        }

        [Fact]
        public void RegularLoginFailure()
        {
            const string password = "Not" + PASSWORD;

            bool actual = Sut.VerifyPassword(null, password);

            Assert.False(actual);
        }

        [Fact]
        public void GenerateToken()
        {
            const string userName = "donkey";

            string token = Sut.GenerateToken(userName);

            Assert.False(string.IsNullOrEmpty(token));
            Assert.Equal(2, token.Count(a => a == '.'));
            Assert.StartsWith("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9", token);
            // todo Validate token's payload and signature.
        }
    }
}