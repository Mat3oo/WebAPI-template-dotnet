using Microsoft.EntityFrameworkCore;
using ToDoOrganizer.Domain.Models;

namespace ToDoOrganizer.Infrastructure.DAL
{
    internal class AppDbContext : DbContext
    {
        public DbSet<Project> Projects => Set<Project>();
        public DbSet<ToDoItem> ToDoItems => Set<ToDoItem>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>().HasQueryFilter(entity => entity.DeleteDate == null);
            modelBuilder.Entity<ToDoItem>().HasQueryFilter(entity => entity.DeleteDate == null);
        }
    }
}