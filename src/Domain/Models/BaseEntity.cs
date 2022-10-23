using System.ComponentModel.DataAnnotations;

namespace ToDoOrganizer.Domain.Models
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public Guid? CreatedBy { get; set; }

        public DateTime? UpdateDate { get; set; }

        public Guid? UpdatedBy { get; set; }

        public DateTime? DeleteDate { get; set; }

        public Guid? DeletedBy { get; set; }
    }
}
