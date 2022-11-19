using ToDoOrganizer.Backend.WebAPI;
using ToDoOrganizer.Backend.WebAPI.Filters;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddServices(builder.Configuration);
    builder.Services.AddControllers(options => options.Filters.Add<OperationCanceledExceptionFilter>());
}

var app = builder.Build();
{
    app.UseExceptionHandler("/api/error");
    app.UseSwagger();
    app.UseSwaggerUI();

    app.MapControllers();
}

app.Run();
