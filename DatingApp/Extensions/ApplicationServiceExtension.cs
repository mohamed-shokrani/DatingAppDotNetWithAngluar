using DatingApp.Data;
using DatingApp.Interfaces;
using DatingApp.Services;

namespace DatingApp.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<ITokenServices, TokenService>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }

    }
}
