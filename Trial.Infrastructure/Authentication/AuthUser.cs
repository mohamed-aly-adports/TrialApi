using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Trial.Infrastructure.Authentication
{
    public static class AuthUser
    {
        public static IHttpContextAccessor? HttpContextAccessor { get; set; }

        public static HttpContext? CurrentHttpContext => HttpContextAccessor?.HttpContext;

        public static Guid UserId =>
            HttpContextAccessor
                .HttpContext?
                .User
                .GetUserId() ??
            throw new ApplicationException("User context is unavailable");

        public static Guid GetUserId(this ClaimsPrincipal? principal)
        {
            string? userId = principal?.FindFirstValue(ClaimTypes.NameIdentifier);

            return Guid.TryParse(userId, out Guid parsedUserId) ?
                parsedUserId :
                throw new ApplicationException("User id is unavailable");
        }
    }
}
