using System.ComponentModel.DataAnnotations;

namespace ToDoOrganizer.Domain.Models
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }

        private DateTime? _createdDate;
        [Required]
        public DateTime CreatedDate
        {
            get { return _createdDate ?? DateTime.UtcNow; }
            set { _createdDate = value; }
        }

        [Required]
        public Guid? CreatedBy { get; set; }

        public DateTime? UpdateDate { get; set; }

        public Guid? UpdatedBy { get; set; }

        public DateTime? DeleteDate { get; set; }

        public Guid? DeletedBy { get; set; }
    }
}
