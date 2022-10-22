using ToDoOrganizer.Application.Interfaces.Other;

namespace ToDoOrganizer.Application.Services.Other
{
    public sealed class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
        public DateTime Now => DateTime.Now;
    }
}
