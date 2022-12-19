using Microsoft.AspNetCore.OData;
using ToDoOrganizer.Backend.WebAPI;
using ToDoOrganizer.Backend.WebAPI.Filters;
using ToDoOrganizer.Backend.WebAPI.Helpers;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddServices(builder.Configuration);
    builder.Services
        .AddControllers(options => options.Filters.Add<OperationCanceledExceptionFilter>())
        .AddOData(options =>
            options.AddRouteComponents("odata", ODataHelper.GetEdmModelForProject())
                .Select()
                .Filter()
                .Count()
                .OrderBy()
                .SetMaxTop(10));
}

var app = builder.Build();
{
    app.UseExceptionHandler("/api/error");
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers()
        .RequireAuthorization("DefaultSecurity");
}

app.Run();
