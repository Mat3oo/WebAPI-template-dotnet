using Microsoft.AspNetCore.Mvc;
using ToDoOrganizer.Backend.Contracts.V1.Requests.Filters;
using ToDoOrganizer.Backend.Contracts.V1;
using ToDoOrganizer.Backend.Contracts.V1.Requests;
using ToDoOrganizer.Backend.Contracts.V1.Responses;
using ToDoOrganizer.Backend.WebAPI.Helpers;
using ToDoOrganizer.Backend.WebAPI.Interfaces.Services;
using MapsterMapper;
using ToDoOrganizer.Backend.Domain.Aggregates;
using ToDoOrganizer.Backend.Application.Interfaces.Services;
using ToDoOrganizer.Backend.Application.Interfaces.DAL.Repositories;
using ToDoOrganizer.Backend.Application.Models.Project;

namespace ToDoOrganizer.Backend.WebAPI.Controllers;

[ApiController]
[ApiVersion("1.0")]
public class ProjectController : ControllerBase
{
    private readonly IProjectService _projectService;
    private readonly IGenericReadRepository<Project> _readRepo;
    private readonly IMapper _mapper;
    private readonly IUriService _uriService;

    public ProjectController(
        IProjectService projectService,
        IGenericReadRepository<Project> readRepo,
        IMapper mapper,
        IUriService uriService)
    {
        _projectService = projectService ?? throw new ArgumentNullException(nameof(projectService));
        _readRepo = readRepo ?? throw new ArgumentNullException(nameof(readRepo));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _uriService = uriService ?? throw new ArgumentNullException(nameof(uriService));
    }

    [HttpGet(ApiRoutes.Projects.Get, Name = "GetAsync")]
    public async Task<IActionResult> GetAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await _readRepo.GetByIdAsync<ProjectResponse>(id, ct: ct).ConfigureAwait(false);
        if (entity is null)
        {
            return NotFound();
        }

        return Ok(entity);
    }

    [HttpGet(ApiRoutes.Projects.GetAll)]
    public async Task<IActionResult> GetAllPagedAsync([FromQuery] PaginationFilter filter, CancellationToken ct = default)
    {
        var entities = await _readRepo
            .GetAllAsync<ProjectResponse>(new(filter.PageNumber, filter.PageSize), ct: ct)
            .ConfigureAwait(false);

        var count = Convert.ToUInt32(await _readRepo.CountAsync(ct: ct).ConfigureAwait(false));

        var response = PaginationHelper.CreatePagedReponse<ProjectResponse>(
            entities,
            filter,
            count,
            _uriService,
            Request.Path.Value!); //Request Path could be invalid for the calling user when api is behind Gateway

        return Ok(response);
    }

    [HttpPost(ApiRoutes.Projects.Create)]
    public async Task<IActionResult> CreateAsync(ProjectCreateRequest createRequest, CancellationToken ct = default)
    {
        #region Manual validation - this region is here only for example purpouse, cuz automatic validation is used
        // // inject "IValidator<ProjectCreateRequest>" in ctor
        // var validationResult = await _projectCreateRequestValidator.ValidateAsync(createRequest, ct).ConfigureAwait(false);
        // if (!validationResult.IsValid)
        // {
        //     validationResult.AddToModelState(this.ModelState);
        //     return ValidationProblem(ModelState);
        // }
        #endregion

        var mapped = _mapper.Map<ProjectCreateEntity>(createRequest);
        var userId = new Guid(); //TODO: get userId from token claims

        var created = await _projectService.InsertAsync(mapped, userId, ct).ConfigureAwait(false);

        var mappedBack = _mapper.Map<ProjectResponse>(created);

        return CreatedAtRoute(nameof(GetAsync), new { id = mappedBack.Id }, mappedBack);
    }

    [HttpPost(ApiRoutes.Projects.Update)]
    public async Task<IActionResult> UpdateAsync(Guid id, ProjectUpdateRequest updateRequest, CancellationToken ct = default)
    {
        var mapped = _mapper.Map<ProjectUpdateEntity>(updateRequest);
        var userId = new Guid(); //TODO: get userId from token claims

        var isSuccess = await _projectService.UpdateAsync(id, mapped, userId, ct).ConfigureAwait(false);

        return isSuccess ? NoContent() : NotFound();
    }

    [HttpDelete(ApiRoutes.Projects.Delete)]
    public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var userId = new Guid(); //TODO: get userId from token claims

        var isSuccess = await _projectService.DeleteAsync(id, userId, ct).ConfigureAwait(false);

        return isSuccess ? NoContent() : NotFound();
    }
}
