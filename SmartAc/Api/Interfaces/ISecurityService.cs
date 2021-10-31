namespace Api.Interfaces
{
    public interface ISecurityService
    {
        bool VerifyPassword(string userName, string password);
        string GenerateToken(string userName);
    }
}