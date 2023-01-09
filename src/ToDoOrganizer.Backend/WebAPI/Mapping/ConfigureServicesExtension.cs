using Mapster;
using MapsterMapper;

namespace ToDoOrganizer.Backend.WebAPI.Mapping;

public static partial class ConfigureServicesExtension
{
    public static IServiceCollection AddMapper(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(AppDomain.CurrentDomain.GetAssemblies());

        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();

        return services;
    }
}
