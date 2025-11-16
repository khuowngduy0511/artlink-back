using Application.Commons;
using Application.AppConfigurations;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Migrators.PostgreSQL;

public class AppDBContextFactory : IDesignTimeDbContextFactory<AppDBContext>
{
    public AppDBContext CreateDbContext(string[] args)
    {
        // Build configuration - use project directory, not current directory
        var basePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", ".."));
        var appsettingsPath = Path.Combine(basePath, "appsettings.json");
        
        // If appsettings.json doesn't exist in parent, try current directory
        if (!File.Exists(appsettingsPath))
        {
            appsettingsPath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
        }
        
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Path.GetDirectoryName(appsettingsPath) ?? Directory.GetCurrentDirectory())
            .AddJsonFile(Path.GetFileName(appsettingsPath), optional: false)
            .Build();

        // Get connection string from configuration
        var connectionString = configuration.GetConnectionString("MSSQLServerDB");
        
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("Connection string 'MSSQLServerDB' not found in appsettings.json");
        }
        
        Console.WriteLine($"[AppDBContextFactory] Using connection string from appsettings.json");

        // Create AppConfiguration
        var appConfig = new AppConfiguration
        {
            ConnectionStrings = new ConnectionStrings
            {
                MSSQLServerDB = connectionString
            }
        };

        // Create DbContextOptions
        var optionsBuilder = new DbContextOptionsBuilder<AppDBContext>();
        optionsBuilder.UseNpgsql(
            appConfig.ConnectionStrings.MSSQLServerDB,
            x => x.MigrationsAssembly("Migrators.PostgreSQL")
        );

        return new AppDBContext(optionsBuilder.Options, appConfig);
    }
}

