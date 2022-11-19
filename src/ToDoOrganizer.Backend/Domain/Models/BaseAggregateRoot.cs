using ToDoOrganizer.Backend.Domain.Models;

namespace ToDoOrganizer.Backend.Models;

public abstract class BaseAggregateRoot : BaseEntity
{
    protected BaseAggregateRoot(Guid id) : base(id)
    { }
}