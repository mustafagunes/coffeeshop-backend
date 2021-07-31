using Core.Security;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IJwtHelper, JwtHelper>();
            return services;
        }
    }
}