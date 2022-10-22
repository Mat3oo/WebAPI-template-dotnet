using System.ComponentModel.DataAnnotations;

namespace ToDoOrganizer.Domain.Models
{
    public class Project : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        [MaxLength(50)]
        public string? Description { get; set; }

        public ICollection<ToDoItem> ToDoItems { get; set; } = new HashSet<ToDoItem>();
    }
}