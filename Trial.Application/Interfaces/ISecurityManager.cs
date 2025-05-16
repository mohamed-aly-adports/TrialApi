using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Trial.Application.Options;

namespace Trial.Application.Interfaces
{
    public interface ISecurityManager
    {
   //     VerifyPassword()
        string GenerateToken(Claim[] claims);

        /// <summary>
        /// يقوم بفك تشفير JWE باستخدام مفتاح التشفير المحدد.
        /// </summary>
        /// <param name="jweToken">الرمز المشفر</param>
        /// <param name="decryptionKey">مفتاح فك التشفير</param>
        /// <returns>النص المفكك تشفيره</returns>

        string Decrypt(string jweToken, string decryptionKey);
        /// <summary>
        /// يقوم بتشفير نص إلى JWE باستخدام مفتاح التشفير المحدد.
        /// </summary>
        /// <param name="payload">النص الذي سيتم تشفيره</param>
        /// <param name="encryptionKey">مفتاح التشفير</param>
        /// <returns>رمز JWE مشفر</returns>
        string Encrypt(string payload, string encryptionKey);
} 
}