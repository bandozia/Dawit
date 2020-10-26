using Dawit.Infrastructure.Service.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace Dawit.API.Service.Extensions
{
    public static class AuthFactory
    {
        public static ITokenService JWTTokenService(IServiceProvider sp)
        {
            var config = sp.GetRequiredService<IConfiguration>();            
            return new JWTTokenService(config["Jwt:secret"], config.GetValue<int>("Jwt:duration"));
        }

        public static IServiceCollection AddTokenAuthentication(this IServiceCollection services, string secret)
        {
            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(opts =>
            {
                opts.RequireHttpsMetadata = false;
                opts.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true
                };
            });

            return services;
        }
    }
}
