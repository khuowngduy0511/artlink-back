using Application.Commons;
using Application.AppConfigurations;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Migrators.MSSQL;

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
                    ?? "Server=localhost;Database=ArtLinkDB;Trusted_Connection=True;TrustServerCertificate=True;"
            }
        };

        // Create DbContextOptions
        var optionsBuilder = new DbContextOptionsBuilder<AppDBContext>();
        optionsBuilder.UseSqlServer(
            appConfig.ConnectionStrings.MSSQLServerDB,
            x => x.MigrationsAssembly("Migrators.MSSQL")
        );

        return new AppDBContext(optionsBuilder.Options, appConfig);
    }
}
