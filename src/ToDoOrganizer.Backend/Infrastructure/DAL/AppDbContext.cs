using Microsoft.EntityFrameworkCore;
using ToDoOrganizer.Backend.Domain.Aggregates;
using ToDoOrganizer.Backend.Domain.Entities;
using ToDoOrganizer.Backend.Infrastructure.DAL.Configurations;

namespace ToDoOrganizer.Backend.Infrastructure.DAL;

internal class AppDbContext : DbContext
{
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<ToDoItem> ToDoItems => Set<ToDoItem>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected AppDbContext(DbContextOptions options) : base(options) { } // it's necessary due to allow the correct non generic ctor to be selected in inheritance

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ToDoItemConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectConfiguration());
    }
}
