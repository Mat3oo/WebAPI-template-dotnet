using ToDoOrganizer.Backend.Application.Models.Project;
using ToDoOrganizer.Backend.Domain.Aggregates;

namespace ToDoOrganizer.Backend.Application.Interfaces.Services;

public interface IProjectService
{
    Task<Project> InsertAsync(ProjectCreateEntity newEntity, Guid userId, CancellationToken ct = default);
    Task<bool> UpdateAsync(Guid id, ProjectUpdateEntity newEntity, Guid userId, CancellationToken ct = default);
    Task<bool> DeleteAsync(Guid id, Guid userId, CancellationToken ct = default);
}