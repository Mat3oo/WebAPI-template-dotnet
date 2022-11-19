using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ToDoOrganizer.Backend.Application.Interfaces.DAL;
using ToDoOrganizer.Backend.Application.Interfaces.Other;
using ToDoOrganizer.Backend.Infrastructure.DAL;
using ToDoOrganizer.Backend.Infrastructure.Services.Other;

namespace Infrastructure;

public static class ConfigureServicesExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var provider = configuration.GetValue("DbProvider", "InMemory");

        switch (provider)
        {
            case "Sqlite":
                {
                    // dotnet ef migrations add "MigrationName" -c SqliteAppDbContext -o ../Infrastructure/DAL/Migrations/SqliteAppDb -s ../WebAPI -- --DbProvider=Sqlite
                    services.AddDbContext<SqliteAppDbContext>(options => options.UseSqlite(
                        configuration.GetConnectionString("Sqlite"),
                        b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

                    var dbContextDescriptor =
                        services.Where(x => x.ServiceType == typeof(SqliteAppDbContext)).Single();

                    services.Replace(new ServiceDescriptor(typeof(AppDbContext),
                        typeof(SqliteAppDbContext),
                        dbContextDescriptor.Lifetime));
                }
                break;
            case "PostgreSql":
                {
                    services.AddDbContext<PostgreSqlAppDbContext>(options => options.UseNpgsql(
                        configuration.GetConnectionString("PostgreSql"),
                        b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

                    var dbContextDescriptor =
                        services.Where(x => x.ServiceType == typeof(PostgreSqlAppDbContext)).Single();

                    services.Replace(new ServiceDescriptor(typeof(AppDbContext),
                        typeof(PostgreSqlAppDbContext),
                        dbContextDescriptor.Lifetime));
                }
                break;
            case "InMemory":
                services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("MemoryDb"));
                break;
            default:
                throw new Exception($"Unsupported provider: {provider}");
        }

        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
