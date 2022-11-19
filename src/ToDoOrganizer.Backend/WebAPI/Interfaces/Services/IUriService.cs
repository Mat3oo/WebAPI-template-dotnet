using ToDoOrganizer.Backend.Contracts.V1.Filters;

namespace ToDoOrganizer.Backend.WebAPI.Interfaces.Services
{
    public interface IUriService
    {
        Uri GetPageUri(PaginationFilter filter, string route);
    }
}