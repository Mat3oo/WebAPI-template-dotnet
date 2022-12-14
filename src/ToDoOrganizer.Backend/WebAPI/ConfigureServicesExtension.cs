using Application;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using ToDoOrganizer.Backend.WebAPI.Configuration.Models;
using ToDoOrganizer.Backend.WebAPI.Interfaces.Services;
using ToDoOrganizer.Backend.WebAPI.Mapping;
using ToDoOrganizer.Backend.WebAPI.Services;

namespace ToDoOrganizer.Backend.WebAPI;

public static partial class ConfigureServicesExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMapper();

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

        services.AddValidatorsFromAssembly(typeof(Contracts.V1.ApiRoutes).Assembly);
        services.AddFluentValidationAutoValidation(options => options.DisableDataAnnotationsValidation = true); //auto validation is not recommended, manual validation approach should be used instead, but this project will use ProblemDetails as response for all 500 and 400 errors to be comply with standard

        var authConfig = configuration.GetSection(AuthenticationConfiguration.SectionName)
            .Get<AuthenticationConfiguration>()!;

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = authConfig.Authority.AbsoluteUri;
                options.TokenValidationParameters.ValidAudience = authConfig.ValidAudience;
            });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("DefaultSecurity", policy =>
            {
                policy.RequireAuthenticatedUser();
            });
        });

        return services;
    }
}
