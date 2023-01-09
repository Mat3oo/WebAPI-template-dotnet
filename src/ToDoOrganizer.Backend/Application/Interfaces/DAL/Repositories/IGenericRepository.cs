using ToDoOrganizer.Backend.Domain.Models;

namespace ToDoOrganizer.Backend.Application.Interfaces.DAL.Repositories;

public interface IGenericRepository<TEntity> :
    IGenericReadRepository<TEntity>,
    IGenericCommandRepository<TEntity>
where TEntity : BaseEntity
{ }
