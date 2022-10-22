using ToDoOrganizer.Contracts.V1.Filters;

namespace ToDoOrganizer.WebAPI.Interfaces.Services
{
    public interface IUriService
    {
        Uri GetPageUri(PaginationFilter filter, string route);
    }
}