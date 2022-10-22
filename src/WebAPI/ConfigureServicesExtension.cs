using Application;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using ToDoOrganizer.WebAPI.Interfaces.Services;
using ToDoOrganizer.WebAPI.Services;

namespace ToDoOrganizer.WebAPI
{
    public static class ConfigureServicesExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(Program));

            services.AddHttpContextAccessor();
            services.AddSingleton<IUriService>(o =>
                {
                    var accessor = o.GetRequiredService<IHttpContextAccessor>();
                    var request = accessor.HttpContext?.Request;
                    var uri = string.Concat(request?.Scheme, "://", request?.Host.ToUriComponent());
                    return new UriService(uri);
                });

            services.AddApplication(configuration);
            services.AddInfrastructure(configuration);

            services.AddControllers();

            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new QueryStringApiVersionReader("api-version"),
                    new HeaderApiVersionReader("X-Version"));
                options.ReportApiVersions = true;
            });

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }
    }
}