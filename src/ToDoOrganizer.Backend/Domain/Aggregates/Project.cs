using ToDoOrganizer.Backend.Domain.Entities;
using ToDoOrganizer.Backend.Domain.Models;

namespace ToDoOrganizer.Backend.Domain.Aggregates;

public class Project : BaseAggregateRoot
{
    public Project(Guid id) : base(id)
    {
    }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public ICollection<ToDoItem> ToDoItems { get; set; } = new HashSet<ToDoItem>();
}
