using Microsoft.AspNetCore.WebUtilities;
using ToDoOrganizer.Backend.Contracts.V1.Filters;
using ToDoOrganizer.Backend.WebAPI.Interfaces.Services;

namespace ToDoOrganizer.Backend.WebAPI.Services
{
    public class UriService : IUriService
    {
        private readonly string _baseUri;

        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }

        public Uri GetPageUri(PaginationFilter filter, string route)
        {
            var _enpointUri = new Uri(string.Concat(_baseUri, route));

            var modifiedUri = QueryHelpers.AddQueryString(_enpointUri.ToString(),
                nameof(filter.PageNumber), filter.PageNumber.ToString());

            modifiedUri = QueryHelpers.AddQueryString(modifiedUri,
                nameof(filter.PageSize), filter.PageSize.ToString());

            return new Uri(modifiedUri);
        }
    }
}