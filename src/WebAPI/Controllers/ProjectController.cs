using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ToDoOrganizer.Application.Interfaces.DAL;
using ToDoOrganizer.Contracts.V1.Filters;
using ToDoOrganizer.Contracts.V1;
using ToDoOrganizer.Contracts.V1.Requests;
using ToDoOrganizer.Contracts.V1.Responses;
using ToDoOrganizer.Contracts.V1.Responses.Wrappers;
using ToDoOrganizer.Domain.Models;
using ToDoOrganizer.WebAPI.Helpers;
using ToDoOrganizer.WebAPI.Interfaces.Services;

namespace ToDoOrganizer.WebAPI.Controllers
{
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
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var entity = await _uow.ToDoItemRepo.GetByIdAsync<ProjectResponse>(id).ConfigureAwait(false);
            if (entity is null)
            {
                return NotFound();
            }

            var response = new Response<ProjectResponse>(entity);
            return Ok(response);
        }

        [HttpGet(ApiRoutes.Projects.GetAll)]
        public async Task<IActionResult> GetAllPagedAsync([FromQuery] PaginationFilter filter)
        {
            var entities = await _uow.ToDoItemRepo.GetAllAsync<ProjectResponse>(new(filter.PageNumber, filter.PageSize))
                .ConfigureAwait(false);

            var count = Convert.ToUInt32(await _uow.ToDoItemRepo.CountAsync().ConfigureAwait(false));

            var response = PaginationHelper.CreatePagedReponse<ProjectResponse>(
                entities,
                filter,
                count,
                _uriService,
                Request.Path.Value!); //Request Path could be invalid when api is behind Gateway

            return Ok(response);
        }

        [HttpPost(ApiRoutes.Projects.Create)]
        public async Task<IActionResult> CreateAsync(ProjectCreateRequest sample)
        {
            var mapped = _mapper.Map<ToDoItem>(sample);

            var userId = new Guid(); //TODO: get userId from token claims

            _uow.ToDoItemRepo.Insert(mapped, userId);
            await _uow.ToDoItemRepo.SaveChangesAsync().ConfigureAwait(false);

            var mappedBack = _mapper.Map<ProjectResponse>(mapped);

            return CreatedAtRoute(nameof(GetAsync), new { id = mapped.Id }, mappedBack);
        }

        [HttpPost(ApiRoutes.Projects.Update)]
        public async Task<IActionResult> UpdateAsync(Guid id, ProjectUpdateRequest sample)
        {
            var entity = await _uow.ToDoItemRepo.GetByIdAsync(id).ConfigureAwait(false);
            if (entity is default(ToDoItem))
            {
                return NotFound();
            }

            var mapped = _mapper.Map<ToDoItem>(sample);
            mapped.Id = id;

            var userId = new Guid(); //TODO: get userId from token claims

            await _uow.ToDoItemRepo.UpdateAsync(mapped, userId).ConfigureAwait(false);
            await _uow.ToDoItemRepo.SaveChangesAsync().ConfigureAwait(false);

            return NoContent();
        }

        [HttpDelete(ApiRoutes.Projects.Delete)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var entity = await _uow.ToDoItemRepo.GetByIdAsync(id).ConfigureAwait(false);
            if (entity is default(ToDoItem))
            {
                return NotFound();
            }

            var userId = new Guid(); //TODO: get userId from token claims

            await _uow.ToDoItemRepo.DeleteSoftAsync(id, userId).ConfigureAwait(false);
            await _uow.ToDoItemRepo.SaveChangesAsync().ConfigureAwait(false);

            return NoContent();
        }
    }
}
