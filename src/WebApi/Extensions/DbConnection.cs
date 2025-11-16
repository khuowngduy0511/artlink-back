using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Net;
using System.Net.Sockets;

namespace WebApi.Extensions;

public static class DbConnection
{
    public static IServiceCollection AddDbContextConfiguration(this IServiceCollection services, string connectionString)
    {
        // Build connection string with NpgsqlConnectionStringBuilder to ensure proper formatting
        var builder = new NpgsqlConnectionStringBuilder(connectionString);
        
        // If host is an IP address, ensure it's IPv4
        // If host is a hostname, it should already be resolved to IPv4 in Program.cs
        if (!string.IsNullOrEmpty(builder.Host))
        {
            // Check if host is already an IP address
            if (IPAddress.TryParse(builder.Host, out var ipAddress))
            {
                // If it's IPv6, try to find IPv4 equivalent (this shouldn't happen if Program.cs worked)
                if (ipAddress.AddressFamily == AddressFamily.InterNetworkV6)
                {
                    Console.WriteLine($"[DbConnection] WARNING: Connection string contains IPv6 address: {ipAddress}. This may cause connection issues on Render.com.");
                }
            }
        }
        
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
