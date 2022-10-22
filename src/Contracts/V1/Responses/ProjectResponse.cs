namespace ToDoOrganizer.Contracts.V1.Responses
{
    public class ProjectResponse
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}
