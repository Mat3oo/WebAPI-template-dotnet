using System.ComponentModel.DataAnnotations;

namespace ToDoOrganizer.Domain.Models
{
    public class ToDoItem : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public bool IsCompleted { get; set; } = false;

        public Guid? ProjectId { get; set; }

        public Project? Project { get; set; }
    }
}