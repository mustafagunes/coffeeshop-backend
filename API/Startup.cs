using System.Net;
using Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using Service.Extensions;

namespace CoffeeShop.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(setupAction =>
            {
                setupAction.ReturnHttpNotAcceptable = true;
            })
            .AddNewtonsoftJson(setupAction =>
            {
                setupAction.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });
            
            // Connect Database
            // dotnet ef migrations add initial-migration -s ../API/
            // dotnet ef database update -s ../API/
            services.AddDbContext<AppDbContext>(options =>
            {
                var connStr = Configuration.GetConnectionString("Db");
                options.UseNpgsql(connStr, o =>
                {
                    o.MigrationsAssembly("Data");
                });         
            });

            services.AddApplicationServices();
            
            // Add Identity Services
            services.AddIdentityServices(Configuration);
            
            // IPokemonRepository Added
            // services.AddScoped<IPokemonRepository, PokemonRepository>();
            
            // Swagger Implementation
            services.AddSwaggerDocumentation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            } 
            else
            {
                // Production Error Handling
                app.UseExceptionHandler(builder =>
                {
                    builder.Run(async context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                        var error = context.Features.Get<IExceptionHandlerFeature>();
                        if (error != null)
                        {
                            context.Response.AddApplicationError(error.Error.Message);
                            await context.Response.WriteAsync(error.Error.Message);
                        }
                    });
                });
            }
            
            app.UseHttpsRedirection();

            app.UseRouting();
            
            app.UseAuthentication();
            app.UseAuthorization();

            // Swagger
            app.UseSwaggerDocumention();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
    
    public static class ApplicationError
    {
        public static void AddApplicationError(this HttpResponse response, string message)
        {
            response.Headers.Add("Application-Error", message);
            response.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
        }
    }
}