namespace ToDoOrganizer.Contracts.V1.Requests
{
    public class ProjectUpdateRequest
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}
