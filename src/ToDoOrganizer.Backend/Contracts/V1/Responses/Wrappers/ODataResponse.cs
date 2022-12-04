using System.Text.Json.Serialization;

namespace ToDoOrganizer.Backend.Contracts.V1.Responses.Wrappers;

public record ODataResponse<T>(
    IEnumerable<T> Value,
    [property: JsonPropertyName("@odata.count")] int? Count = null,
    [property: JsonPropertyName("@odata.context")] string? Context = null)
    where T : class;
