using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Extensions;

internal static class ApplyMigrations
{
    internal static IApplicationBuilder UseApplyMigrations(this IApplicationBuilder app)
    {
        try
        {
            using var scope = app.ApplicationServices.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<AppDBContext>();
            
            // Try to check if database is accessible
            if (context.Database.CanConnect())
            {
                var pendingMigrations = context.Database.GetPendingMigrations().ToList();
                if (pendingMigrations.Any())
                {
                    Console.WriteLine($"[MIGRATION] Found {pendingMigrations.Count} pending migration(s):");
                    foreach (var migration in pendingMigrations)
                    {
                        Console.WriteLine($"[MIGRATION]   - {migration}");
                    }
                    Console.WriteLine("[MIGRATION] Applying migrations...");
                    context.Database.Migrate();
                    Console.WriteLine("[MIGRATION] Migrations applied successfully.");
                }
                else
                {
                    Console.WriteLine("[MIGRATION] Database is up to date. No pending migrations.");
                }
            }
            else
            {
                Console.WriteLine("[MIGRATION] Warning: Cannot connect to database. Skipping migrations.");
            }
        }
        catch (Exception ex)
        {
            // Log error but don't crash the application
            Console.WriteLine($"[MIGRATION] Error applying migrations: {ex.Message}");
            Console.WriteLine("[MIGRATION] Application will continue to start without applying migrations.");
        }
        
        return app;
    }
}