using ToDoOrganizer.Application.Interfaces.Other;

namespace ToDoOrganizer.Infrastructure.Services.Other;

internal sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
    public DateTime Now => DateTime.Now;
}
