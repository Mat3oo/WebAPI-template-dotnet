namespace ToDoOrganizer.Backend.Domain.Exceptions;

public class CreationConstraintException : DomainException
{

    public CreationConstraintException()
        : base()
    {
    }

    public CreationConstraintException(string message)
        : base(message)
    {
    }

    public CreationConstraintException(string? message, Exception? inner)
        : base(message, inner)
    {
    }
}
