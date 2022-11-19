using Microsoft.EntityFrameworkCore;

namespace ToDoOrganizer.Backend.Infrastructure.DAL;

internal class SqliteAppDbContext : AppDbContext
{
    public SqliteAppDbContext(DbContextOptions<SqliteAppDbContext> options) : base(options)
    {
    }
}
