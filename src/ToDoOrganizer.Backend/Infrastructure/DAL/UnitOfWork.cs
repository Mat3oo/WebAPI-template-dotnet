using MapsterMapper;
using ToDoOrganizer.Backend.Application.Interfaces.DAL;
using ToDoOrganizer.Backend.Application.Interfaces.DAL.Repositories;
using ToDoOrganizer.Backend.Application.Interfaces.Other;
using ToDoOrganizer.Backend.Domain.Aggregates;
using ToDoOrganizer.Backend.Domain.Entities;
using ToDoOrganizer.Backend.Infrastructure.DAL.Repositories;

namespace ToDoOrganizer.Backend.Infrastructure.DAL;

internal class UnitOfWork : IUnitOfWork
{
    public IGenericRepository<ToDoItem> ToDoItemRepo { get; init; }
    public IGenericRepository<Project> ProjectRepo { get; init; }

    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public UnitOfWork(AppDbContext dbContext, IDateTimeProvider dateTimeProvider, IMapper mapper)
    {
        if (dateTimeProvider is null)
        {
            throw new ArgumentNullException(nameof(dateTimeProvider));
        }

        _context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        ToDoItemRepo = new GenericRepository<AppDbContext, ToDoItem>(_context, dateTimeProvider, _mapper);
        ProjectRepo = new GenericRepository<AppDbContext, Project>(_context, dateTimeProvider, _mapper);
    }


    public Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        return _context.SaveChangesAsync(ct);
    }
}
