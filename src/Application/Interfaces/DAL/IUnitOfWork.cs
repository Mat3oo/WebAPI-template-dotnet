using ToDoOrganizer.Application.Interfaces.DAL.Repositories;
using ToDoOrganizer.Domain.Aggregates;
using ToDoOrganizer.Domain.Entities;

namespace ToDoOrganizer.Application.Interfaces.DAL;

public interface IUnitOfWork
{
    IGenericRepository<ToDoItem> ToDoItemRepo { get; init; }
    IGenericRepository<Project> ProjectRepo { get; init; }
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}
