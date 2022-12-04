namespace ToDoOrganizer.Backend.Contracts.V1.Responses;

public record ProjectResponse(Guid Id, string Name, string? Description);
