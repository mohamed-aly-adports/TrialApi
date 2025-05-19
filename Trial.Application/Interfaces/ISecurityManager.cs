using System.Security.Claims;

namespace Trial.Application.Interfaces
{
    public interface ISecurityManager
    {
        string GenerateToken(Claim[] claims);
        string Decrypt(string jweToken, string decryptionKey);
        string Encrypt(string payload, string encryptionKey);
        string Hash(string inputPassword);
        bool VerifyPassword(string inputPassword, string hashPassword);
    }
}