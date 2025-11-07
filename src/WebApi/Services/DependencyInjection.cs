using Application.Services.Abstractions;

namespace WebApi.Services;

public static class DependencyInjection
{
    public static IServiceCollection AddServiceDIs(this IServiceCollection services)
    {
        services.AddScoped<IClaimService, ClaimService>();
        return services;
    }
}
