using System.Linq.Expressions;
using ToDoOrganizer.Backend.Domain.Models;
using ToDoOrganizer.Backend.Domain.Filters;

namespace ToDoOrganizer.Backend.Application.Interfaces.DAL.Repositories;

public interface IGenericReadRepository<TEntity> where TEntity : BaseEntity
{
    Task<List<TEntity>> GetAllAsync(PaginationFilter? filter = null,
        bool includeSoftDeleted = false, CancellationToken ct = default);
    Task<List<MapDest>> GetAllAsync<MapDest>(PaginationFilter? filter = null,
        bool includeSoftDeleted = false, CancellationToken ct = default);
    IQueryable<MapDest> GetAllQueryable<MapDest>(bool includeSoftDeleted = false);
    Task<TEntity?> GetByIdAsync(Guid id, bool includeSoftDeleted = false, CancellationToken ct = default);
    Task<MapDest?> GetByIdAsync<MapDest>(Guid id, bool includeSoftDeleted = false, CancellationToken ct = default);
    Task<List<TEntity>> GetByConditionAsync(Expression<Func<TEntity, bool>> predicate,
         PaginationFilter? filter = null, bool includeSoftDeleted = false, CancellationToken ct = default);
    Task<List<MapDest>> GetByConditionAsync<MapDest>(Expression<Func<TEntity, bool>> predicate,
        PaginationFilter? filter = null, bool includeSoftDeleted = false, CancellationToken ct = default);
    Task<long> CountAsync(bool includeSoftDeleted = false, CancellationToken ct = default);
}
