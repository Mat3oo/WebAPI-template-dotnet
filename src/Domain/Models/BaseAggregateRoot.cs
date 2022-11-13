using ToDoOrganizer.Domain.Models;

namespace ToDoOrganizer.Models;

public abstract class BaseAggregateRoot : BaseEntity
{
    protected BaseAggregateRoot(Guid id) : base(id)
    { }
}