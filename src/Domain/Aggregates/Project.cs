using System.ComponentModel.DataAnnotations;
using ToDoOrganizer.Domain.Entities;
using ToDoOrganizer.Models;

namespace ToDoOrganizer.Domain.Aggregates;

public class Project : BaseAggregateRoot
{
    public Project(Guid id) : base(id)
    {
    }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = null!;

    [MaxLength(50)]
    public string? Description { get; set; }

    public ICollection<ToDoItem> ToDoItems { get; set; } = new HashSet<ToDoItem>();
}
