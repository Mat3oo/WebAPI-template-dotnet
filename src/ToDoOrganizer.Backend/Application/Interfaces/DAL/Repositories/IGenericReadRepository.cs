using System.Linq.Expressions;
using ToDoOrganizer.Backend.Domain.Models;
using ToDoOrganizer.Backend.Domain.Filters;

namespace ToDoOrganizer.Backend.Application.Interfaces.DAL.Repositories;

public interface IGenericReadRepository<TEntity> where TEntity : BaseEntity
{
    Task<List<TEntity>> GetAllAsync(PaginationFilter? filter = null, CancellationToken ct = default);
    Task<List<MapDest>> GetAllAsync<MapDest>(PaginationFilter? filter = null, CancellationToken ct = default);
    IQueryable<MapDest> GetAllQueryable<MapDest>();
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<MapDest?> GetByIdAsync<MapDest>(Guid id, CancellationToken ct = default);
    Task<List<TEntity>> GetByConditionAsync(Expression<Func<TEntity, bool>> predicate,
         PaginationFilter? filter = null, CancellationToken ct = default);
    Task<List<MapDest>> GetByConditionAsync<MapDest>(Expression<Func<TEntity, bool>> predicate,
        PaginationFilter? filter = null, CancellationToken ct = default);
    Task<long> CountAsync(CancellationToken ct = default);
}
