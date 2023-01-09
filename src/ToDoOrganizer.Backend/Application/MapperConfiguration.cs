using Mapster;
using ToDoOrganizer.Backend.Application.Models.Project;
using ToDoOrganizer.Backend.Domain.Aggregates;

namespace ToDoOrganizer.Backend.Application;

class MapperConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ProjectCreateEntity, Project>()
            .ConstructUsing(c => new(Guid.Empty));

        config.NewConfig<ProjectUpdateEntity, Project>();
    }
}
