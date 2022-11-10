namespace ToDoOrganizer.Contracts.V1.Responses.Wrappers
{
    public class PagedResponse<T>
    {
        public T Data { get; set; }
        public uint PageNumber { get; set; }
        public uint PageSize { get; set; }
        public Uri FirstPage { get; set; } = null!;
        public Uri LastPage { get; set; } = null!;
        public uint TotalPages { get; set; }
        public uint TotalRecords { get; set; }
        public Uri NextPage { get; set; } = null!;
        public Uri PreviousPage { get; set; } = null!;

        public PagedResponse(T data, uint pageNumber, uint pageSize)
        {
            this.Data = data;
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }
    }
}
