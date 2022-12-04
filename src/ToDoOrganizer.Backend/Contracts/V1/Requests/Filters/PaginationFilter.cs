namespace ToDoOrganizer.Backend.Contracts.V1.Requests.Filters;

public class PaginationFilter
{
    private const uint _maxPageSize = 10;

    private uint _pageNumber = 1;
    public uint PageNumber
    {
        get => _pageNumber;
        set => _pageNumber = (value < 1) ? 1 : value;
    }

    private uint _pageSize = _maxPageSize;
    public uint PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > _maxPageSize) ? _maxPageSize : value;
    }

    public PaginationFilter()
    {
    }

    public PaginationFilter(uint pageNumber, uint pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}
