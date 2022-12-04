using Mapster;
using ToDoOrganizer.Backend.Contracts.V1.Requests;
using ToDoOrganizer.Backend.Contracts.V1.Responses;
using ToDoOrganizer.Backend.Domain.Aggregates;

namespace ToDoOrganizer.Backend.WebAPI.Mapping;

class MapperConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Project, ProjectResponse>();
        config.NewConfig<Project, ProjectODataResponse>();

        config.NewConfig<ProjectCreateRequest, Project>()
            .ConstructUsing(c => new Project(Guid.Empty));

        config.NewConfig<ProjectUpdateRequest, Project>();
    }
}
