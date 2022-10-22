using AutoMapper;
using ToDoOrganizer.Application.Interfaces.DAL;
using ToDoOrganizer.Application.Interfaces.DAL.Repositories;
using ToDoOrganizer.Application.Interfaces.Other;
using ToDoOrganizer.Domain.Models;
using ToDoOrganizer.Infrastructure.DAL.Repositories;

namespace ToDoOrganizer.Infrastructure.DAL
{
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

            ToDoItemRepo = new GenericRepository<ToDoItem>(_context, dateTimeProvider, mapper);
            ProjectRepo = new GenericRepository<Project>(_context, dateTimeProvider, mapper);
        }


        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}