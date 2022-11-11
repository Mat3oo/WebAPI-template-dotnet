using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ToDoOrganizer.Application.Interfaces.DAL.Repositories;
using ToDoOrganizer.Domain.Models;
using ToDoOrganizer.Domain.Filters;
using ToDoOrganizer.Application.Interfaces.Other;
using MapsterMapper;
using Mapster;

namespace ToDoOrganizer.Infrastructure.DAL.Repositories;

internal class GenericRepository<TContext, TEntity> : IGenericRepository<TEntity>
where TContext : DbContext
where TEntity : BaseEntity
{
    private readonly TContext _context;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IMapper _mapper;
    private readonly DbSet<TEntity> _entities;

    public GenericRepository(TContext context, IDateTimeProvider dateTimeProvider, IMapper mapper)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _entities = context.Set<TEntity>();
    }

    public Task<List<TEntity>> GetAllAsync(PaginationFilter? filter = null, CancellationToken ct = default)
    {
        if (filter is null)
        {
            return _entities.AsNoTracking().ToListAsync(ct);
        }

        var skip = Convert.ToInt32((filter.PageNumber - 1) * filter.PageSize);
        var pageSize = Convert.ToInt32(filter.PageSize);

        return _entities.AsNoTracking()
            .OrderBy(k => k.Id)
            .Skip(skip)
            .Take(pageSize)
            .ToListAsync(ct);
    }

    public Task<List<MapDest>> GetAllAsync<MapDest>(PaginationFilter? filter = null, CancellationToken ct = default)
    {
        if (filter is null)
        {
            var queryWithoutPaging = _entities.AsNoTracking();
            var projectionWithoutPaging = _mapper.From(queryWithoutPaging).ProjectToType<MapDest>();

            return projectionWithoutPaging.ToListAsync(ct);
        }

        var skip = Convert.ToInt32((filter.PageNumber - 1) * filter.PageSize);
        var pageSize = Convert.ToInt32(filter.PageSize);

        var query = _entities.AsNoTracking()
                        .OrderBy(k => k.Id)
                        .Skip(skip)
                        .Take(pageSize);
        var projection = _mapper.From(query).ProjectToType<MapDest>();

        return projection.ToListAsync(ct);
    }

    public Task<TEntity?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return _entities.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id, ct);
    }

    public Task<MapDest?> GetByIdAsync<MapDest>(Guid id, CancellationToken ct = default)
    {
        var query = _entities.AsNoTracking().Where(p => p.Id == id);
        var projection = _mapper.From(query).ProjectToType<MapDest>();

        return projection.FirstOrDefaultAsync(ct);
    }

    public Task<List<TEntity>> GetByConditionAsync(Expression<Func<TEntity, bool>> predicate,
        PaginationFilter? filter = null, CancellationToken ct = default)
    {
        if (filter is null)
        {
            return _entities.AsNoTracking().Where(predicate).ToListAsync(ct);
        }

        var skip = Convert.ToInt32((filter.PageNumber - 1) * filter.PageSize);
        var pageSize = Convert.ToInt32(filter.PageSize);

        return _entities.AsNoTracking()
            .Where(predicate)
            .OrderBy(k => k.Id)
            .Skip(skip)
            .Take(pageSize)
            .ToListAsync(ct);
    }

    public Task<List<MapDest>> GetByConditionAsync<MapDest>(Expression<Func<TEntity, bool>> predicate,
         PaginationFilter? filter = null, CancellationToken ct = default)
    {
        if (filter is null)
        {
            var queryWithoutPaging = _entities.AsNoTracking().Where(predicate);
            var projectionWithoutPaging = _mapper.From(queryWithoutPaging).ProjectToType<MapDest>();

            return projectionWithoutPaging.ToListAsync(ct);
        }

        var skip = Convert.ToInt32((filter.PageNumber - 1) * filter.PageSize);
        var pageSize = Convert.ToInt32(filter.PageSize);

        var query = _entities.AsNoTracking()
            .Where(predicate)
            .OrderBy(k => k.Id)
            .Skip(skip)
            .Take(pageSize);
        var projection = _mapper.From(query).ProjectToType<MapDest>();

        return projection.ToListAsync(ct);
    }

    public void Insert(TEntity entity, Guid userId)
    {
        entity.CreatedBy = userId;
        entity.CreatedDate = _dateTimeProvider.UtcNow;
        _entities.Add(entity);
    }

    public void Update(TEntity entity, Guid userId)
    {
        //1st approach - updated entity is not accessible beyond this update call:
        // var persistenEntity = await _entities.AsTracking()
        //     .FirstAsync(p => p.Id == entity.Id)
        //     .ConfigureAwait(false);

        // _context.Entry(persistenEntity!).CurrentValues.SetValues(entity);
        // persistenEntity.UpdateDate = _dateTimeProvider.UtcNow;
        // persistenEntity.UpdatedBy = userId;
        // _entities.Update(persistenEntity);

        //2nd approach - entity stays attached and accessible beoynd scope of this update call:
        entity.UpdateDate = _dateTimeProvider.UtcNow;
        entity.UpdatedBy = userId;
        _entities.Attach(entity).State = EntityState.Modified;
    }

    public void Delete(TEntity entity)
    {
        _entities.Remove(entity);
    }

    public void DeleteSoft(TEntity entity, Guid userId)
    {
        entity.DeleteDate = _dateTimeProvider.UtcNow;
        entity.DeletedBy = userId;
        _entities.Update(entity);
    }

    public Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        return _context.SaveChangesAsync(ct);
    }

    public Task<long> CountAsync(CancellationToken ct = default)
    {
        return _entities.LongCountAsync(ct);
    }
}
