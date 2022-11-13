using System.ComponentModel.DataAnnotations;
using ToDoOrganizer.Domain.Common.Models;

namespace ToDoOrganizer.Domain.Models;

public abstract class BaseEntity : Entity
{
    protected BaseEntity(Guid id) : base(id)
    { }

    [Required]
    public DateTime CreatedDate { get; set; }

    [Required]
    public Guid? CreatedBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeleteDate { get; set; }

    public Guid? DeletedBy { get; set; }
}
