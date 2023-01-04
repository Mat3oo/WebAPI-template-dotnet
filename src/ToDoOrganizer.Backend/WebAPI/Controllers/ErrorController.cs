using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ToDoOrganizer.Backend.Domain.Exceptions;

namespace ToDoOrganizer.Backend.WebAPI.Controllers;

[ApiController]
public class ErrorController : ControllerBase
{
    [Route("api/error")]
    public IActionResult Error()
    {
        Exception exception = HttpContext.Features.Get<IExceptionHandlerFeature>()!.Error;

        if (exception is DomainException ex)
        {
            return Problem(detail: ex.Message);
        }

        return Problem();
    }
}