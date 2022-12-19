namespace ToDoOrganizer.Backend.WebAPI.Configuration.Models;

public sealed record AuthenticationConfiguration(
    Uri Authority,
    string ValidAudience)
    {
        public const string SectionName = "Authentication";
    };
