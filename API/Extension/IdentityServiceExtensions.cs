using System.Text;
using AutoMapper.Configuration;
using Core.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CoffeeShop.API.Extension
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, JwtSettings jwtSettings)
        {
            services.AddAuthentication(a => {

                a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                a.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                
            }).AddJwtBearer(builder => {

                builder.Audience = jwtSettings.Audience;
                builder.RequireHttpsMetadata = false;
                builder.SaveToken = true;
                builder.ClaimsIssuer = jwtSettings.Issuer;
                builder.TokenValidationParameters = new TokenValidationParameters() {

                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,                            
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
                };
            });

            return services;
        }
    }
}