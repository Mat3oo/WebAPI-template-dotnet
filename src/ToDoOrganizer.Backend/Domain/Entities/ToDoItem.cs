using System.ComponentModel.DataAnnotations;
using ToDoOrganizer.Backend.Domain.Aggregates;
using ToDoOrganizer.Backend.Domain.Models;

namespace ToDoOrganizer.Backend.Domain.Entities;

public class ToDoItem : BaseEntity
{
    public ToDoItem(Guid id) : base(id)
    { }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = null!;

    [Required]
    [MaxLength(50)]
    public bool IsCompleted { get; set; } = false;

    public Guid? ProjectId { get; set; }

    public Project? Project { get; set; }
}
