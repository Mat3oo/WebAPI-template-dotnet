using Microsoft.EntityFrameworkCore;

namespace ToDoOrganizer.Infrastructure.DAL;

internal class PostgreSqlAppDbContext : AppDbContext
{
    public PostgreSqlAppDbContext(DbContextOptions<PostgreSqlAppDbContext> options) : base(options)
    {
    }
}
