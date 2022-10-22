using AutoMapper;
using ToDoOrganizer.Contracts.V1.Requests;
using ToDoOrganizer.Contracts.V1.Responses;
using ToDoOrganizer.Domain.Models;

namespace ToDoOrganizer.WebAPI.MappingProfiles
{
    class AutomapperConfiguration : Profile
    {
        public AutomapperConfiguration()
        {
            CreateMap<Project, ProjectResponse>();
            CreateMap<ProjectCreateRequest, Project>();
            CreateMap<ProjectUpdateRequest, Project>();
        }
    }
}
