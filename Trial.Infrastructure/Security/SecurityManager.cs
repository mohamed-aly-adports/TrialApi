using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Jose;
using Microsoft.IdentityModel.Tokens;
using Trial.Application.Interfaces;
using Trial.Application.Options;
using BCrypt.Net

namespace Trial.Infrastructure.Security
{
    public class SecurityManager : ISecurityManager
    {
        private readonly JwtSetting _token;

        public bool VerifyPassword(string inputPassword, string hashedPassword)
        {
            //return Verify(inputPassword, hashedPassword);
            return true;
        }

        public SecurityManager(JwtSetting token)
        {
            _token = token;
        }

        public string GenerateToken(Claim[] claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_token.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _token.Issuer,
                audience: _token.Issuer,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_token.ExpiryMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// يقوم بفك تشفير JWE باستخدام مفتاح التشفير المحدد.
        /// </summary>
        /// <param name="jweToken">الرمز المشفر</param>
        /// <param name="decryptionKey">مفتاح فك التشفير</param>
        /// <returns>النص المفكك تشفيره</returns>
        public string Decrypt(string jweToken, string decryptionKey)
        {
            try
            {
                // تحويل المفتاح لنمط بايت (يمكن تغيير طريقة التحويل حسب نوع المفتاح)
                var keyBytes = System.Text.Encoding.UTF8.GetBytes(decryptionKey);

                // استخدم الخوارزمية المناسبة: يتم هنا استخدام A256GCMKW كمثال
                string payload = JWT.Decode(jweToken, keyBytes, JweAlgorithm.A256GCMKW, JweEncryption.A256GCM);
                return payload;
            }
            catch (Exception ex)
            {
                throw new Exception("Error decrypting JWE token", ex);
            }
        }

        /// <summary>
        /// يقوم بتشفير نص إلى JWE باستخدام مفتاح التشفير المحدد.
        /// </summary>
        /// <param name="payload">النص الذي سيتم تشفيره</param>
        /// <param name="encryptionKey">مفتاح التشفير</param>
        /// <returns>رمز JWE مشفر</returns>
        public string Encrypt(string payload, string encryptionKey)
        {
            try
            {
                var keyBytes = System.Text.Encoding.UTF8.GetBytes(encryptionKey);

                // تشفير باستخدام الخوارزمية المناسبة. هنا مثال على تشفير باستخدام A256GCMKW
                string jwe = JWT.Encode(payload, keyBytes, JweAlgorithm.A256GCMKW, JweEncryption.A256GCM);
                return jwe;
            }
            catch (Exception ex)
            {
                throw new Exception("Error encrypting payload to JWE", ex);
            }
        }
    }
}
