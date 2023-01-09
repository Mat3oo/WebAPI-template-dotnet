using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDoOrganizer.Backend.Application.Interfaces.Services;
using ToDoOrganizer.Backend.Application.Services;

namespace Application;

public static class ConfigureServicesExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IProjectService, ProjectService>();

        return services;
    }
}
