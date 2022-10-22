using System.Linq.Expressions;
using ToDoOrganizer.Domain.Models;
using ToDoOrganizer.Domain.Filters;

namespace ToDoOrganizer.Application.Interfaces.DAL.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        Task<List<TEntity>> GetAllAsync(PaginationFilter? filter = null);
        Task<List<MapDest>> GetAllAsync<MapDest>(PaginationFilter? filter = null);
        Task<TEntity?> GetByIdAsync(Guid id);
        Task<MapDest?> GetByIdAsync<MapDest>(Guid id);
        Task<List<TEntity>> GetByConditionAsync(Expression<Func<TEntity, bool>> predicate,
             PaginationFilter? filter = null);
        Task<List<MapDest>> GetByConditionAsync<MapDest>(Expression<Func<TEntity, bool>> predicate,
            PaginationFilter? filter = null);
        void Insert(TEntity entity, Guid userId);
        Task UpdateAsync(TEntity entity, Guid userId);
        Task DeleteSoftAsync(Guid id, Guid userId);
        Task DeleteAsync(Guid id, Guid userId);
        Task<long> CountAsync();
        Task<int> SaveChangesAsync();
    }
}