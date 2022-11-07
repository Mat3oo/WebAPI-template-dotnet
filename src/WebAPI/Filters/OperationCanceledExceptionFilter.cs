using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ToDoOrganizer.WebAPI.Filters;

public class OperationCanceledExceptionFilter : ExceptionFilterAttribute
{
    private readonly ILogger _logger;

    public OperationCanceledExceptionFilter(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<OperationCanceledExceptionFilter>();
    }
    public override void OnException(ExceptionContext context)
    {
        if(context.Exception is OperationCanceledException)
        {
            _logger.LogInformation("Request was cancelled");
            context.ExceptionHandled = true;
            context.Result = new StatusCodeResult(499);
        }
    }
}
