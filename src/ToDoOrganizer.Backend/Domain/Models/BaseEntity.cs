using ToDoOrganizer.Backend.Domain.Common.Models;

namespace ToDoOrganizer.Backend.Domain.Models;

public abstract class BaseEntity : Entity
{
    protected BaseEntity(Guid id) : base(id)
    { }

    public DateTime CreatedDate { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeleteDate { get; set; }

    public Guid? DeletedBy { get; set; }
}
