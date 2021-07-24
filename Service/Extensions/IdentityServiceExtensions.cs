using System;
using System.Text;
using Core.Model.Auth;
using Data.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Service.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
        {
            var builder = services.AddIdentityCore<AppUser>(opt =>
            {
                opt.SignIn.RequireConfirmedEmail = true;
                opt.Lockout.MaxFailedAccessAttempts = 7; // The number of failed login attempts allowed before the account is locked out.
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1); // Specifies the amount of the time the account should be locked.
            });

            builder = new IdentityBuilder(builder.UserType, builder.Services);
            builder.AddRoles<IdentityRole>();
            builder.AddEntityFrameworkStores<AppDbContext>(); // Retrieve user info
            builder.AddSignInManager<SignInManager<AppUser>>();
            builder.AddDefaultTokenProviders(); // Otherwise => Generate tokens for email confirmation, password reset, two factor authentication
            
            // Password Options Rules
            services.Configure<IdentityOptions>(opt =>
            {
                opt.Password.RequiredLength = 8;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireDigit = false;
                opt.Password.RequireUppercase = false;
            });
            
            // Add JWT Token
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true, // Validate signature of the token
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Token:Key"])),
                        ValidIssuer = config["Token:Issuer"],
                        ValidateIssuer = true, // Validate the server that generates the token.
                        ValidateAudience = false, // Validate the recipient of the token is authorized to receive
                        ValidateLifetime = true // ValidateLifetime = true / Check if the token is not expired and the signing key of the issuer is valid
                    };
                });

            return services;
        }
    }
}