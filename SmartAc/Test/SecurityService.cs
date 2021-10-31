using Api.Interfaces;

namespace Test
{
    public class SecurityService:ISecurityService
    {
        public bool VerifyPassword(string userName, string password)
        {
            throw new System.NotImplementedException();
        }

        public string GenerateToken(string userName)
        {
            throw new System.NotImplementedException();
        }
    }
}