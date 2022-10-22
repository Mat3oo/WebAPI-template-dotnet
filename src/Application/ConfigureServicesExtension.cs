using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDoOrganizer.Application.Interfaces.Other;
using ToDoOrganizer.Application.Services.Other;

namespace Application
{
    public static class ConfigureServicesExtension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

            return services;
        }
    }    
}