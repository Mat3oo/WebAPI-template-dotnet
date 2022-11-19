using ToDoOrganizer.Backend.Application.Interfaces.Other;

namespace ToDoOrganizer.Backend.Infrastructure.Services.Other;

internal sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
    public DateTime Now => DateTime.Now;
}
