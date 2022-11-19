using System.ComponentModel.DataAnnotations;
using ToDoOrganizer.Backend.Domain.Entities;
using ToDoOrganizer.Backend.Models;

namespace ToDoOrganizer.Backend.Domain.Aggregates;

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
