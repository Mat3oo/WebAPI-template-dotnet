using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ToDoOrganizer.Backend.Application.Interfaces.DAL.Repositories;
using ToDoOrganizer.Backend.Domain.Models;
using ToDoOrganizer.Backend.Domain.Filters;
using ToDoOrganizer.Backend.Application.Interfaces.Other;
using MapsterMapper;
using Mapster;

namespace ToDoOrganizer.Backend.Infrastructure.DAL.Repositories;

internal class GenericRepository<TContext, TEntity> : IGenericRepository<TEntity>
where TContext : DbContext
where TEntity : BaseEntity
{
    private readonly TContext _context;
    protected readonly IDateTimeProvider DateTimeProvider;
    protected readonly IMapper Mapper;
    protected readonly TypeAdapterConfig MapperConfig;
    protected readonly DbSet<TEntity> Entities;

    public GenericRepository(
        TContext context,
        IDateTimeProvider dateTimeProvider,
        IMapper mapper,
        TypeAdapterConfig mapperConfig)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        DateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        MapperConfig = mapperConfig ?? throw new ArgumentNullException(nameof(mapperConfig));
        Entities = context.Set<TEntity>();
    }

    public Task<List<TEntity>> GetAllAsync(PaginationFilter? filter = null, CancellationToken ct = default)
    {
        if (filter is null)
        {
            return Entities.AsNoTracking().ToListAsync(ct);
        }

        var skip = Convert.ToInt32((filter.PageNumber - 1) * filter.PageSize);
        var pageSize = Convert.ToInt32(filter.PageSize);

        return Entities.AsNoTracking()
            .OrderBy(k => k.Id)
            .Skip(skip)
            .Take(pageSize)
            .ToListAsync(ct);
    }

    public Task<List<MapDest>> GetAllAsync<MapDest>(PaginationFilter? filter = null, CancellationToken ct = default)
    {
        if (filter is null)
        {
            var queryWithoutPaging = Entities.AsNoTracking();
            var projectionWithoutPaging = Mapper.From(queryWithoutPaging).ProjectToType<MapDest>();

            return projectionWithoutPaging.ToListAsync(ct);
        }

        var skip = Convert.ToInt32((filter.PageNumber - 1) * filter.PageSize);
        var pageSize = Convert.ToInt32(filter.PageSize);

        var query = Entities.AsNoTracking()
                        .OrderBy(k => k.Id)
                        .Skip(skip)
                        .Take(pageSize);
        var projection = Mapper.From(query).ProjectToType<MapDest>();

        return projection.ToListAsync(ct);
    }

    public IQueryable<MapDest> GetAllQueryable<MapDest>()
    {
        var query = Entities.AsNoTracking()
            .ProjectToType<MapDest>(MapperConfig);

        return query;
    }

    public Task<TEntity?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return Entities.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id, ct);
    }

    public Task<MapDest?> GetByIdAsync<MapDest>(Guid id, CancellationToken ct = default)
    {
        var query = Entities.AsNoTracking().Where(p => p.Id == id);
        var projection = Mapper.From(query).ProjectToType<MapDest>();

        return projection.FirstOrDefaultAsync(ct);
    }

    public Task<List<TEntity>> GetByConditionAsync(Expression<Func<TEntity, bool>> predicate,
        PaginationFilter? filter = null, CancellationToken ct = default)
    {
        if (filter is null)
        {
            return Entities.AsNoTracking().Where(predicate).ToListAsync(ct);
        }

        var skip = Convert.ToInt32((filter.PageNumber - 1) * filter.PageSize);
        var pageSize = Convert.ToInt32(filter.PageSize);

        return Entities.AsNoTracking()
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
            var queryWithoutPaging = Entities.AsNoTracking().Where(predicate);
            var projectionWithoutPaging = Mapper.From(queryWithoutPaging).ProjectToType<MapDest>();

            return projectionWithoutPaging.ToListAsync(ct);
        }

        var skip = Convert.ToInt32((filter.PageNumber - 1) * filter.PageSize);
        var pageSize = Convert.ToInt32(filter.PageSize);

        var query = Entities.AsNoTracking()
            .Where(predicate)
            .OrderBy(k => k.Id)
            .Skip(skip)
            .Take(pageSize);
        var projection = Mapper.From(query).ProjectToType<MapDest>();

        return projection.ToListAsync(ct);
    }

    public void Insert(TEntity entity, Guid userId)
    {
        entity.CreatedBy = userId;
        entity.CreatedDate = DateTimeProvider.UtcNow;
        Entities.Add(entity);
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
        entity.UpdateDate = DateTimeProvider.UtcNow;
        entity.UpdatedBy = userId;
        Entities.Attach(entity).State = EntityState.Modified;
    }

    public void Delete(TEntity entity)
    {
        Entities.Remove(entity);
    }

    public void DeleteSoft(TEntity entity, Guid userId)
    {
        entity.DeleteDate = DateTimeProvider.UtcNow;
        entity.DeletedBy = userId;
        Entities.Update(entity);
    }

    public Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        return _context.SaveChangesAsync(ct);
    }

    public Task<long> CountAsync(CancellationToken ct = default)
    {
        return Entities.LongCountAsync(ct);
    }
}
