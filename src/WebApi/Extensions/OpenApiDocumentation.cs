using Microsoft.OpenApi.Models;

namespace WebApi.Extensions;

internal static class OpenApiDocumentation
{
    internal static IServiceCollection AddOpenApiDocumentation(this IServiceCollection services)
    {
        services.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc("v1", new OpenApiInfo 
            { 
                Title = "ArtLink System APIs", 
                Version = "v1",
                Description = "API documentation for ArtLink - Art Sharing Platform"
            });
            
            // Ignore obsolete errors
            opt.IgnoreObsoleteActions();
            opt.IgnoreObsoleteProperties();
            
            // Custom operation IDs to avoid conflicts
            opt.CustomOperationIds(e => 
                $"{e.ActionDescriptor.RouteValues["controller"]}_{e.ActionDescriptor.RouteValues["action"]}");
            
            opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "bearer"
            });
            opt.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type=ReferenceType.SecurityScheme,
                        Id="Bearer"
                    }
                },
                new string[]{}
                }
            });
        });

        services.AddRouting(options => options.LowercaseUrls = true);

        return services;
    }

    internal static IApplicationBuilder UseOpenApi(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        return app;
    }
}
