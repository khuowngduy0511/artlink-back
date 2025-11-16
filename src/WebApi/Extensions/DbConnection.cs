using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace WebApi.Extensions;

public static class DbConnection
{
    public static IServiceCollection AddDbContextConfiguration(this IServiceCollection services, string connectionString)
    {
        // Build connection string with NpgsqlConnectionStringBuilder to ensure proper formatting
        var builder = new NpgsqlConnectionStringBuilder(connectionString);
        
        // Force IPv4 resolution by ensuring we use the hostname (DNS will resolve to IPv4 if available)
        // Npgsql will prefer IPv4 when both are available
        
        services.AddDbContext<AppDBContext>(opt =>
            opt.UseNpgsql(builder.ConnectionString,
                        npgsqlOptions =>
                        {
                            npgsqlOptions.MigrationsAssembly("Migrators.PostgreSQL");
                            // Enable retry on failure for transient errors
                            npgsqlOptions.EnableRetryOnFailure(
                                maxRetryCount: 3,
                                maxRetryDelay: TimeSpan.FromSeconds(5),
                                errorCodesToAdd: null);
                        }));
        return services;
    }
}
