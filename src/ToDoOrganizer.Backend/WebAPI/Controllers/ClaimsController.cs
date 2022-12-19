using Microsoft.AspNetCore.Mvc;

namespace ToDoOrganizer.Backend.WebAPI.Controllers;

[ApiController]
[ApiVersion("1.0")]
public class ClaimsController : ControllerBase
{
    [HttpGet("/api/claims")]
    public IActionResult Get()
    {
        return new JsonResult(User.Claims.Select(s => new { s.Type, s.Value }));
    }
}
