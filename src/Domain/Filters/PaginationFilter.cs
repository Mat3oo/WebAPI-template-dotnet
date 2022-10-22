namespace ToDoOrganizer.Domain.Filters
{
    public class PaginationFilter
    {
        public uint PageNumber { get; }
        public uint PageSize { get; }

        public PaginationFilter(uint pageNumber, uint pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
