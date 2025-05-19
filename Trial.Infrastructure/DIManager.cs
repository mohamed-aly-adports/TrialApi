using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Trial.Infrastructure.Time;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Trial.Application.Interfaces;

namespace Trial.Infrastructure
{
    public static class DIManager
    {
    //public static IServiceCollection AddInfrastructure(
    //    this IServiceCollection services,
    //    IConfiguration configuration){
    //    services
    //        .AddServices()
    //        //.AddDatabase(configuration)
    //        //.AddHealthChecks(configuration)
    //        .AddAuthenticationInternal(configuration)
    //        .AddAuthorizationInternal();
    //    }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<DateTimeProvider>();

            return services;
        }

        private static IServiceCollection AddAuthenticationInternal(
     this IServiceCollection services,
     IConfiguration configuration)
        {
            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddJwtBearer(o =>
            //    {
            //        o.RequireHttpsMetadata = false;
            //        o.TokenValidationParameters = new TokenValidationParameters
            //        {
            //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]!)),
            //            ValidIssuer = configuration["Jwt:Issuer"],
            //            ValidAudience = configuration["Jwt:Audience"],
            //            ClockSkew = TimeSpan.Zero
            //        };
            //    });

            //services.AddHttpContextAccessor();
            //services.AddScoped<IUserContext, UserContext>();
            //services.AddSingleton<ISecurityManager, SecurityManager>();

            return services;
        }

        private static IServiceCollection AddAuthorizationInternal(this IServiceCollection services)
        {
            services.AddAuthorization();

            //services.AddScoped<PermissionProvider>();

            //services.AddTransient<IAuthorizationHandler, PermissionAuthorizationHandler>();

            //services.AddTransient<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();

            return services;
        }


    }
}
