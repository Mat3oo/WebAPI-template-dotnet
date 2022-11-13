using Microsoft.AspNetCore.Mvc;
using ToDoOrganizer.Application.Interfaces.DAL;
using ToDoOrganizer.Contracts.V1.Filters;
using ToDoOrganizer.Contracts.V1;
using ToDoOrganizer.Contracts.V1.Requests;
using ToDoOrganizer.Contracts.V1.Responses;
using ToDoOrganizer.WebAPI.Helpers;
using ToDoOrganizer.WebAPI.Interfaces.Services;
using MapsterMapper;
using ToDoOrganizer.Domain.Aggregates;

namespace ToDoOrganizer.WebAPI.Controllers;

[ApiController]
[ApiVersion("1.0")]
public class ProjectController : ControllerBase
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly IUriService _uriService;

    public ProjectController(IUnitOfWork unitOfWork, IMapper mapper, IUriService uriService)
    {
        _uow = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _uriService = uriService ?? throw new ArgumentNullException(nameof(uriService));
    }

    [HttpGet(ApiRoutes.Projects.Get, Name = "GetAsync")]
    public async Task<IActionResult> GetAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await _uow.ProjectRepo.GetByIdAsync<ProjectResponse>(id, ct).ConfigureAwait(false);
        if (entity is null)
        {
            return NotFound();
        }

        return Ok(entity);
    }

    [HttpGet(ApiRoutes.Projects.GetAll)]
    public async Task<IActionResult> GetAllPagedAsync([FromQuery] PaginationFilter filter, CancellationToken ct = default)
    {
        var entities = await _uow.ProjectRepo
            .GetAllAsync<ProjectResponse>(new(filter.PageNumber, filter.PageSize), ct)
            .ConfigureAwait(false);

        var count = Convert.ToUInt32(await _uow.ProjectRepo.CountAsync(ct).ConfigureAwait(false));

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

        var mapped = _mapper.Map<Project>(createRequest);

        var userId = new Guid(); //TODO: get userId from token claims

        _uow.ProjectRepo.Insert(mapped, userId);
        await _uow.ProjectRepo.SaveChangesAsync(CancellationToken.None).ConfigureAwait(false);

        var mappedBack = _mapper.Map<ProjectResponse>(mapped);

        return CreatedAtRoute(nameof(GetAsync), new { id = mapped.Id }, mappedBack);
    }

    [HttpPost(ApiRoutes.Projects.Update)]
    public async Task<IActionResult> UpdateAsync(Guid id, ProjectUpdateRequest updateRequest, CancellationToken ct = default)
    {
        var entity = await _uow.ProjectRepo.GetByIdAsync(id, ct).ConfigureAwait(false);
        if (entity is default(Project))
        {
            return NotFound();
        }

        var mapped = _mapper.Map(updateRequest, entity);

        var userId = new Guid(); //TODO: get userId from token claims

        _uow.ProjectRepo.Update(mapped, userId);
        await _uow.ProjectRepo.SaveChangesAsync(CancellationToken.None).ConfigureAwait(false);

        return NoContent();
    }

    [HttpDelete(ApiRoutes.Projects.Delete)]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var entity = await _uow.ProjectRepo.GetByIdAsync(id).ConfigureAwait(false);
        if (entity is default(Project))
        {
            return NotFound();
        }

        var userId = new Guid(); //TODO: get userId from token claims

        _uow.ProjectRepo.DeleteSoft(entity, userId);
        await _uow.ProjectRepo.SaveChangesAsync().ConfigureAwait(false);

        return NoContent();
    }
}
