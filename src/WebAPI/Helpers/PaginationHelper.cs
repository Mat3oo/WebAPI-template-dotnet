using ToDoOrganizer.Contracts.V1.Filters;
using ToDoOrganizer.Contracts.V1.Responses.Wrappers;
using ToDoOrganizer.WebAPI.Interfaces.Services;

namespace ToDoOrganizer.WebAPI.Helpers
{
    class PaginationHelper
    {
        public static PagedResponse<List<T>> CreatePagedReponse<T>(
            List<T> pagedData,
            PaginationFilter filter,
            uint totalRecords,
            IUriService uriService,
            string route)
        {
            var respose = new PagedResponse<List<T>>(pagedData, filter.PageNumber, filter.PageSize);

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