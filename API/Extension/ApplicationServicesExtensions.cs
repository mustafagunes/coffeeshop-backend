using Core.Interface;
using Core.Security;
using Data.Repository;
using Data.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace CoffeeShop.API.Extension
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IJwtHelper, JwtHelper>();
            return services;
        }
    }
}