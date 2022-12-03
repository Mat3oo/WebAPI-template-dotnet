namespace ToDoOrganizer.Backend.Domain.Models;

public abstract class BaseAggregateRoot : BaseEntity
{
    protected BaseAggregateRoot(Guid id) : base(id)
    { }
}