using System.Linq.Expressions;
using ToDoOrganizer.Domain.Models;
using ToDoOrganizer.Domain.Filters;

namespace ToDoOrganizer.Application.Interfaces.DAL.Repositories;

public interface IGenericRepository<TEntity> where TEntity : BaseEntity
{
    Task<List<TEntity>> GetAllAsync(PaginationFilter? filter = null, CancellationToken ct = default);
    Task<List<MapDest>> GetAllAsync<MapDest>(PaginationFilter? filter = null, CancellationToken ct = default);
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<MapDest?> GetByIdAsync<MapDest>(Guid id, CancellationToken ct = default);
    Task<List<TEntity>> GetByConditionAsync(Expression<Func<TEntity, bool>> predicate,
         PaginationFilter? filter = null, CancellationToken ct = default);
    Task<List<MapDest>> GetByConditionAsync<MapDest>(Expression<Func<TEntity, bool>> predicate,
        PaginationFilter? filter = null, CancellationToken ct = default);
    void Insert(TEntity entity, Guid userId);
    void Update(TEntity entity, Guid userId);
    void Delete(TEntity entity);
    void DeleteSoft(TEntity entity, Guid userId);
    Task<long> CountAsync(CancellationToken ct = default);
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}
