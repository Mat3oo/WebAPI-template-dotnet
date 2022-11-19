using Microsoft.EntityFrameworkCore;

namespace ToDoOrganizer.Backend.Infrastructure.DAL;

internal class PostgreSqlAppDbContext : AppDbContext
{
    public PostgreSqlAppDbContext(DbContextOptions<PostgreSqlAppDbContext> options) : base(options)
    {
    }
}
