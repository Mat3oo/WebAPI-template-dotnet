namespace ToDoOrganizer.Backend.Contracts.V1.Requests;

public record ProjectUpdateRequest(string Name, string? Description);
