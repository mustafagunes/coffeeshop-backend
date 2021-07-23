using Core.Interface.Auth;
using Microsoft.Extensions.DependencyInjection;
using Service.Service;

namespace Service.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            return services;
        }
    }
}