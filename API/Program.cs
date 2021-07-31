using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CoffeeShop.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // // Added seeds
            // var host = CreateHostBuilder(args).Build();
            // using (var scope = host.Services.CreateScope())
            // {
            //     var services = scope.ServiceProvider;
            //     var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            //     try
            //     {
            //         var userManager = services.GetRequiredService<UserManager<AppUser>>();
            //         var userRole = services.GetRequiredService<RoleManager<IdentityRole>>();
            //
            //         var identityContext = services.GetRequiredService<AppDbContext>();
            //         await identityContext.Database.MigrateAsync(); // create db if it is not there
            //         await AuthSeed.SeedUsersAsync(userManager, userRole);
            //     }
            //     catch (Exception ex)
            //     {
            //         var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
            //         logger.LogError(ex, "An error occured during migration");
            //     }
            // }
            //
            // host.Run();
            
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}