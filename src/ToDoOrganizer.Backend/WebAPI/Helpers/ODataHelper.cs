using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using ToDoOrganizer.Backend.Contracts.V1.Responses;

namespace ToDoOrganizer.Backend.WebAPI.Helpers;

static class ODataHelper
{
    public static IEdmModel GetEdmModelForProject()
    {
        ODataConventionModelBuilder builder = new();
        builder.EntitySet<ProjectResponse>("Projects");
        return builder.GetEdmModel();
    }
}