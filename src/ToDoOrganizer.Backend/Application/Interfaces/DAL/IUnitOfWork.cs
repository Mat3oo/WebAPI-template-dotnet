using ToDoOrganizer.Backend.Application.Interfaces.DAL.Repositories;
using ToDoOrganizer.Backend.Domain.Aggregates;
using ToDoOrganizer.Backend.Domain.Entities;

namespace ToDoOrganizer.Backend.Application.Interfaces.DAL;

public interface IUnitOfWork
{
    IGenericRepository<ToDoItem> ToDoItemRepo { get; init; }
    IGenericRepository<Project> ProjectRepo { get; init; }
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}
