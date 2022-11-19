namespace ToDoOrganizer.Backend.Contracts.V1.Requests;

public record ProjectCreateRequest(string Name, string? Description);
