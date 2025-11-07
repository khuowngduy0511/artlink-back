using Application.Commons;
using Application.Services.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WebApi.Extensions;

internal static class JwtConfiguration
{
    internal static IServiceCollection AddJwtConfiguration
        (this IServiceCollection services, AppConfiguration config)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidIssuer = config.JwtConfiguration.Issuer,
                        ValidAudience = config.JwtConfiguration.Audience,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(config.JwtConfiguration.SecretKey))
                    };
                    options.Events = new JwtBearerEvents()
                    {
                        OnTokenValidated = context =>
                        {
                            var tokenHandler = context.HttpContext.RequestServices
                                                .GetRequiredService<ITokenHandler>();
                            return tokenHandler.ValidateAccessTokenAsync(context);
                        },
                        OnAuthenticationFailed = context =>
                        {
                            // todo record log
                            return Task.CompletedTask;
                        }
                    };
                });
        return services;
    }
}
