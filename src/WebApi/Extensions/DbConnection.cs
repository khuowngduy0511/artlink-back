using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Extensions;

public static class DbConnection
{
    public static IServiceCollection AddDbContextConfiguration(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDBContext>(opt =>
            opt.UseSqlServer(connectionString,
                        x => x.MigrationsAssembly("Migrators.MSSQL")));
        return services;
    }
}
