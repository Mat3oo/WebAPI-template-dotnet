using ToDoOrganizer.Backend.Domain.Models;

namespace ToDoOrganizer.Backend.Application.Interfaces.DAL.Repositories;

public interface IGenericCommandRepository<TEntity> where TEntity : BaseEntity
{
    void Insert(TEntity entity, Guid userId);
    void Update(TEntity entity, Guid userId);
    void Delete(TEntity entity);
    void DeleteSoft(TEntity entity, Guid userId);
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}
