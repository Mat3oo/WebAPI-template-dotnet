using Microsoft.AspNetCore.Mvc;
using ToDoOrganizer.Backend.Contracts.V1;
using ToDoOrganizer.Backend.Contracts.V1.Responses;
using ToDoOrganizer.Backend.Domain.Aggregates;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using ToDoOrganizer.Backend.Application.Interfaces.DAL.Repositories;

namespace ToDoOrganizer.Backend.WebAPI.ODataControllers;

[ApiController]
[ApiVersion("1.0")]
public class ProjectODataController : ODataController
{
    private readonly IGenericReadRepository<Project> _readRepo;

    public ProjectODataController(IGenericReadRepository<Project> readRepo)
    {
        _readRepo = readRepo ?? throw new ArgumentNullException(nameof(readRepo));
    }

    [HttpGet(ApiRoutes.Projects.GetAllOData)]
    [EnableQuery(PageSize = 10)]
    public IActionResult GetOdata()
    {
        var entities = _readRepo
            .GetAllQueryable<ProjectODataResponse>();

        return Ok(entities);
    }
}
