using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Trial.Application.DTO;
using Trial.Application.Interfaces;
using Trial.Application.Repository;
using Trial.Domain.Common;

namespace Trial.Application.Services
{
    public class  AuthService
    {
        private readonly IUser _user;
        private readonly ISecurityManager _jwtManager;

        public AuthService(IUser user,ISecurityManager jwtManager)
        {
            _user = user;
            _jwtManager = jwtManager;
        }
        public async Task<string> Login(Login model)
        {
            var user = await _user.GetByUserNameAsync(model.UserName);
            if (user is UserDTO)
            {
                if (!VerifyPassword(user, model.Password))
                    return null;

                // تجهيز الـ Claims التي ستُضمَّن داخل الـ JWT
                var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Name, user.UserName),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim("role", user.Role), // يمكن إضافة صلاحيات المستخدم إن وجدت
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // لتجنب إعادة استخدام التوكنات
    };
                var jqt = _jwtManager.GenerateToken()
            }
            return "";
        }
    }
}
