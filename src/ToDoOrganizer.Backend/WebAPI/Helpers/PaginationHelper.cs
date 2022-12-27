using ToDoOrganizer.Backend.Contracts.V1.Requests.Filters;
using ToDoOrganizer.Backend.Contracts.V1.Responses.Wrappers;
using ToDoOrganizer.Backend.WebAPI.Interfaces.Services;

namespace ToDoOrganizer.Backend.WebAPI.Helpers
{
    class PaginationHelper
    {
        public static PagedResponse<T> CreatePagedReponse<T>(
            IEnumerable<T> pagedData,
            PaginationFilter filter,
            uint totalRecords,
            IUriService uriService,
            string route)
        {
            var respose = new PagedResponse<T>(pagedData, filter.PageNumber, filter.PageSize);

            var totalPages = Convert.ToDouble(totalRecords) / Convert.ToDouble(filter.PageSize);
            var roundedTotalPages = Convert.ToUInt32(Math.Ceiling(totalPages));

            respose.NextPage =
                (filter.PageNumber >= 1 && (filter.PageNumber < roundedTotalPages))
                ? uriService.GetPageUri(new PaginationFilter(filter.PageNumber + 1, filter.PageSize), route)
                : null!;

            respose.PreviousPage =
                (filter.PageNumber - 1 >= 1 && (filter.PageNumber <= roundedTotalPages))
                ? uriService.GetPageUri(new PaginationFilter(filter.PageNumber - 1, filter.PageSize), route)
                : null!;

            respose.FirstPage = uriService.GetPageUri(new PaginationFilter(1, filter.PageSize), route);
            respose.LastPage = uriService.GetPageUri(new PaginationFilter(roundedTotalPages, filter.PageSize), route);
            respose.TotalPages = roundedTotalPages;
            respose.TotalRecords = totalRecords;

            return respose;
        }
    }
}