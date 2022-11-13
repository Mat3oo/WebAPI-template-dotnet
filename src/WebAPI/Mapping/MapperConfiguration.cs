using Mapster;
using ToDoOrganizer.Contracts.V1.Requests;
using ToDoOrganizer.Contracts.V1.Responses;
using ToDoOrganizer.Domain.Aggregates;

namespace ToDoOrganizer.WebAPI.Mapping;

class MapperConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Project, ProjectResponse>();

        config.NewConfig<ProjectCreateRequest, Project>()
            .ConstructUsing(c => new Project(Guid.Empty));

        config.NewConfig<ProjectUpdateRequest, Project>();
    }
}
