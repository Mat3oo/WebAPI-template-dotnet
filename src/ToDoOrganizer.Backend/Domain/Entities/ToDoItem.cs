using ToDoOrganizer.Backend.Domain.Aggregates;
using ToDoOrganizer.Backend.Domain.Models;

namespace ToDoOrganizer.Backend.Domain.Entities;

public class ToDoItem : BaseEntity
{
    public ToDoItem(Guid id) : base(id)
    { }

    public string Name { get; set; } = null!;

    public bool IsCompleted { get; set; } = false;

    public Guid? ProjectId { get; set; }

    public Project? Project { get; set; }
}
