namespace ToDoOrganizer.Backend.Domain.Common.Models;

public abstract class AggregateRoot : Entity
{
    protected AggregateRoot(Guid id) : base(id)
    { }
}