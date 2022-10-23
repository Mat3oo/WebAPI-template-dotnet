using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDoOrganizer.Application.Interfaces.DAL;
using ToDoOrganizer.Infrastructure.DAL;

namespace Infrastructure
{
    public static class ConfigureServicesExtension
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // services.AddDbContext<AppDbContext>(options =>
            //     options.UseSqlite(configuration.GetConnectionString("Sqlite"),
            //         b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

            services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("inMemory"));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}