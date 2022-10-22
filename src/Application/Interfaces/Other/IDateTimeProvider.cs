namespace ToDoOrganizer.Application.Interfaces.Other
{
    public interface IDateTimeProvider
    {
        public DateTime UtcNow { get; }
        public DateTime Now { get; }
    }
}