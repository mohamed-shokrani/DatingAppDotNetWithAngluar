using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DatingApp.Extensions
{
    public static class IdentityServiceExtension
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)

     .AddJwtBearer(options =>
     {
         options.TokenValidationParameters = new TokenValidationParameters
         {
             ValidateIssuerSigningKey = true,
             IssuerSigningKey = new SymmetricSecurityKey
         (Encoding.UTF8.GetBytes(configuration["TokenKey"])),
             ValidateIssuer = false, // Validate issuer is the  API server 
             ValidateAudience = false, // Angluar Application 
                                       //ValidateLifetime = false,
                                       //ValidateIssuerSigningKey = true
         };
     });
            return services;

        }

    }
}
