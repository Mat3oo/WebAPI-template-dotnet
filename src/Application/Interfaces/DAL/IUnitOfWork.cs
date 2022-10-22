using ToDoOrganizer.Application.Interfaces.DAL.Repositories;
using ToDoOrganizer.Domain.Models;

namespace ToDoOrganizer.Application.Interfaces.DAL
{
    public interface IUnitOfWork
    {
        IGenericRepository<ToDoItem> ToDoItemRepo { get; }
        IGenericRepository<Project> ProjectRepo { get; }
        Task<int> SaveChangesAsync();
    }
}