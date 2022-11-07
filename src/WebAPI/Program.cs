using ToDoOrganizer.WebAPI;
using ToDoOrganizer.WebAPI.Filters;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddServices(builder.Configuration);
    builder.Services.AddControllers(options => options.Filters.Add<OperationCanceledExceptionFilter>());
}

var app = builder.Build();
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.MapControllers();
}

app.Run();
