using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Trial.Application.Interfaces;
using Trial.Application.Options;
using System.Security.Claims;
using System.Text;
using Jose;
using Trial.Application.DTO;
using Trial.Domain.Common;

namespace Trial.Infrastructure.Security
{
    public class SecurityManager : ISecurityManager
    {
        private readonly IUser _user;
        private readonly JwtSetting _token;

        public SecurityManager(JwtSetting token, IUser user)
        {
            _token = token;
            _user = user;
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

        public string Decrypt(string jweToken, string decryptionKey)
        {
            try
            {
                // تحويل المفتاح لنمط بايت (يمكن تغيير طريقة التحويل حسب نوع المفتاح)
                var keyBytes = Encoding.UTF8.GetBytes(decryptionKey);

                // استخدم الخوارزمية المناسبة: يتم هنا استخدام A256GCMKW كمثال
                string payload = JWT.Decode(jweToken, keyBytes, JweAlgorithm.A256GCMKW, JweEncryption.A256GCM);
                return payload;
            }
            catch (Exception ex)
            {
                throw new Exception("Error decrypting JWE token", ex);
            }
        }

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

        public string Hash(string inputPassword)
        {
            return BCrypt.Net.BCrypt.HashPassword(inputPassword, workFactor: 12);
        }

        public bool VerifyPassword(string inputPassword, string hashPassword)
        {
            return BCrypt.Net.BCrypt.Verify(inputPassword, hashPassword);
        }

        public async Task<string> HandleJwt(Login model)
        {
            var user = await _user.GetByUserNameAsync(model.UserName);

            if (user is UserDTO)
            {
                if (!VerifyPassword(user.Password, model.Password))
                    return null;

                var claims = new Claim[] {
                                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                                    new Claim(ClaimTypes.Name, user.UserName),
                                    new Claim(ClaimTypes.Email, user.UserName),
                                   //new Claim("role", user.Role), // يمكن إضافة صلاحيات المستخدم إن وجدت
                                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // لتجنب إعادة استخدام التوكنات
                                     };

                return GenerateToken(claims);
            }
            return "";
        }
    }
}
