using Refit;
using ToDoOrganizer.Contracts.V1.Filters;
using ToDoOrganizer.Contracts.V1;
using ToDoOrganizer.Contracts.V1.Requests;
using ToDoOrganizer.Contracts.V1.Responses;
using ToDoOrganizer.Contracts.V1.Responses.Wrappers;

namespace ToDoOrganizer.SDK
{
    public interface IToDoApiV1
    {
        [Get(ApiRoutes.Projects.Get)]
        Task<ApiResponse<ProjectResponse>> GetAsync(Guid id);

        [Get(ApiRoutes.Projects.GetAll)]
        Task<ApiResponse<List<ProjectResponse>>> GetAllPagedAsync([Query] PaginationFilter filter);

        [Post(ApiRoutes.Projects.Create)]
        Task<ApiResponse<ProjectResponse>> CreateAsync([Body] ProjectCreateRequest sample);

        [Post(ApiRoutes.Projects.Update)]
        Task<ApiResponse<ProjectResponse>> UpdateAsync(Guid id, [Body] ProjectUpdateRequest sample);
    }
}