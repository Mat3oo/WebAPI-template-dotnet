namespace ToDoOrganizer.Backend.Contracts.V1.Responses;

public record ProjectODataResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public string? Description { get; init; }
}
