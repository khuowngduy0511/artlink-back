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
        // Build configuration
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        // Create AppConfiguration
        var appConfig = new AppConfiguration
        {
            ConnectionStrings = new ConnectionStrings
            {
                MSSQLServerDB = configuration.GetConnectionString("MSSQLServerDB") 
                    ?? "Host=localhost;Port=5432;Database=ArtLinkDB;Username=postgres;Password=postgres;"
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

