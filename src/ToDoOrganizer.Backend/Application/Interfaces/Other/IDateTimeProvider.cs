namespace ToDoOrganizer.Backend.Application.Interfaces.Other
{
    public interface IDateTimeProvider
    {
        public DateTime UtcNow { get; }
        public DateTime Now { get; }
    }
}