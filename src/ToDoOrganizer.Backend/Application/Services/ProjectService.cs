using MapsterMapper;
using ToDoOrganizer.Backend.Application.Interfaces.DAL;
using ToDoOrganizer.Backend.Application.Interfaces.Services;
using ToDoOrganizer.Backend.Application.Models.Project;
using ToDoOrganizer.Backend.Domain.Aggregates;
using ToDoOrganizer.Backend.Domain.Exceptions;

namespace ToDoOrganizer.Backend.Application.Services;

public class ProjectService : IProjectService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProjectService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<Project> InsertAsync(ProjectCreateEntity newEntity, Guid userId, CancellationToken ct = default)
    {
        var duplicates = await _unitOfWork.ProjectRepo
            .GetByConditionAsync(p => p.Name == newEntity.Name, includeSoftDeleted: true, ct: ct)
            .ConfigureAwait(false);
        if (duplicates.Any())
        {
            throw new CreationConstraintException("Project with given Name already exist");
        }
        var mapped = _mapper.Map<Project>(newEntity);
        _unitOfWork.ProjectRepo.Insert(mapped, userId);

        _ = await _unitOfWork.SaveChangesAsync(CancellationToken.None).ConfigureAwait(false);

        return mapped;
    }

    public async Task<bool> UpdateAsync(Guid id, ProjectUpdateEntity newEntity, Guid userId, CancellationToken ct = default)
    {
        var entity = await _unitOfWork.ProjectRepo.GetByIdAsync(id, ct: ct).ConfigureAwait(false);
        if (entity is default(Project))
        {
            return false;
        }

        var mapped = _mapper.Map(newEntity, entity);

        _unitOfWork.ProjectRepo.Update(mapped, userId);
        var result = await _unitOfWork.ProjectRepo
            .SaveChangesAsync(CancellationToken.None)
            .ConfigureAwait(false);

        return result > 0;
    }

    public async Task<bool> DeleteAsync(Guid id, Guid userId, CancellationToken ct = default)
    {
        var entity = await _unitOfWork.ProjectRepo.GetByIdAsync(id, ct: ct).ConfigureAwait(false);
        if (entity is default(Project))
        {
            return false;
        }

        _unitOfWork.ProjectRepo.DeleteSoft(entity, userId);
        var result = await _unitOfWork.ProjectRepo
            .SaveChangesAsync(CancellationToken.None)
            .ConfigureAwait(false);

        return result > 0;
    }
}