using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Extensions;

internal static class ApplyMigrations
{
    internal static IApplicationBuilder UseApplyMigrations(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<AppDBContext>();
        if (context.Database.GetPendingMigrations().Any())
        {
            context.Database.Migrate();
        }
        return app;
    }
}