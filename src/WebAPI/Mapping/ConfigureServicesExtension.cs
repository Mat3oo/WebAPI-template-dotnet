using System.Reflection;
using Mapster;
using MapsterMapper;

namespace ToDoOrganizer.WebAPI.Mapping;

public static partial class ConfigureServicesExtension
{
    public static IServiceCollection AddMapper(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());

        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();

        return services;
    }
}
