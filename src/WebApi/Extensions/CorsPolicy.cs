namespace WebApi.Extensions;

internal static class CorsPolicy
{
    internal static IServiceCollection AddCorsPolicy(this IServiceCollection services)
    {
        return services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin() // Allow requests from any origin
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });
    }

    internal static IApplicationBuilder UseCorsPolicy(this IApplicationBuilder app)
        => app.UseCors();
}
