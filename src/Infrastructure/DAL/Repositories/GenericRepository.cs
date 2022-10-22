using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ToDoOrganizer.Application.Interfaces.DAL.Repositories;
using ToDoOrganizer.Domain.Models;
using ToDoOrganizer.Domain.Filters;
using ToDoOrganizer.Application.Interfaces.Other;
using AutoMapper;

namespace ToDoOrganizer.Infrastructure.DAL.Repositories
{
    internal class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly AppDbContext _context;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IMapper _mapper;
        private readonly DbSet<TEntity> _entities;

        public GenericRepository(AppDbContext context, IDateTimeProvider dateTimeProvider, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _entities = context.Set<TEntity>();
        }

        public Task<List<TEntity>> GetAllAsync(PaginationFilter? filter = null)
        {
            if (filter is null)
            {
                return _entities.AsNoTracking().ToListAsync();
            }

            var skip = Convert.ToInt32((filter.PageNumber - 1) * filter.PageSize);
            var pageSize = Convert.ToInt32(filter.PageSize);

            return _entities.AsNoTracking()
                .OrderBy(k => k.Id)
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();
        }

        public Task<List<MapDest>> GetAllAsync<MapDest>(PaginationFilter? filter = null)
        {
            if (filter is null)
            {
                var queryWithoutPaging = _entities.AsNoTracking();
                var projectionWithoutPaging = _mapper.ProjectTo<MapDest>(queryWithoutPaging);

                return projectionWithoutPaging.ToListAsync();
            }

            var skip = Convert.ToInt32((filter.PageNumber - 1) * filter.PageSize);
            var pageSize = Convert.ToInt32(filter.PageSize);

            var query = _entities.AsNoTracking()
                            .OrderBy(k => k.Id)
                            .Skip(skip)
                            .Take(pageSize);
            var projection = _mapper.ProjectTo<MapDest>(query);

            return projection.ToListAsync();
        }

        public Task<TEntity?> GetByIdAsync(Guid id)
        {
            return _entities.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<MapDest?> GetByIdAsync<MapDest>(Guid id)
        {
            var query = _entities.AsNoTracking().Where(p => p.Id == id);
            var projection = _mapper.ProjectTo<MapDest>(query);

            return projection.FirstOrDefaultAsync();
        }

        public Task<List<TEntity>> GetByConditionAsync(Expression<Func<TEntity, bool>> predicate,
            PaginationFilter? filter = null)
        {
            if (filter is null)
            {
                return _entities.AsNoTracking().Where(predicate).ToListAsync();
            }

            var skip = Convert.ToInt32((filter.PageNumber - 1) * filter.PageSize);
            var pageSize = Convert.ToInt32(filter.PageSize);

            return _entities.AsNoTracking()
                .Where(predicate)
                .OrderBy(k => k.Id)
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();
        }

        public Task<List<MapDest>> GetByConditionAsync<MapDest>(Expression<Func<TEntity, bool>> predicate,
             PaginationFilter? filter = null)
        {
            if (filter is null)
            {
                var queryWithoutPaging = _entities.AsNoTracking().Where(predicate);
                var projectionWithoutPaging = _mapper.ProjectTo<MapDest>(queryWithoutPaging);

                return projectionWithoutPaging.ToListAsync();
            }

            var skip = Convert.ToInt32((filter.PageNumber - 1) * filter.PageSize);
            var pageSize = Convert.ToInt32(filter.PageSize);

            var query = _entities.AsNoTracking()
                .Where(predicate)
                .OrderBy(k => k.Id)
                .Skip(skip)
                .Take(pageSize);
            var projection = _mapper.ProjectTo<MapDest>(query);

            return projection.ToListAsync();
        }

        public void Insert(TEntity entity, Guid userId)
        {
            entity.CreatedBy = userId;
            _entities.Add(entity);
        }

        public async Task UpdateAsync(TEntity entity, Guid userId)
        {
            var persistenEntity = await _entities.AsTracking()
                .FirstAsync(p => p.Id == entity.Id)
                .ConfigureAwait(false);

            _context.Entry(persistenEntity!).CurrentValues.SetValues(entity);
            persistenEntity.UpdateDate = _dateTimeProvider.UtcNow;
            persistenEntity.UpdatedBy = userId;
            _entities.Update(persistenEntity);

            //2nd approach - "oneliner" (but entity stays attached and accessible beoynd scope of this update call):
            //entity.UpdatTime = _dateTimeProvider.UtcNow;
            //_entities.Attach(entity).State = EntityState.Modified;
        }

        public async Task DeleteAsync(Guid id, Guid userId)
        {
            var persistentEntity = await _entities.AsTracking()
                .FirstAsync(p => p.Id == id)
                .ConfigureAwait(false);

            _entities.Remove(persistentEntity);
        }

        public async Task DeleteSoftAsync(Guid id, Guid userId)
        {
            var persistentEntity = await _entities.AsTracking()
                .FirstAsync(p => p.Id == id)
                .ConfigureAwait(false);

            persistentEntity.DeleteDate = _dateTimeProvider.UtcNow;
            persistentEntity.DeletedBy = userId;
            _entities.Update(persistentEntity);
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public Task<long> CountAsync()
        {
            return _entities.LongCountAsync();
        }
    }
}
