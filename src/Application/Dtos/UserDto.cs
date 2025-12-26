using System.Text.Json.Serialization;

namespace BlogApi.Application.Dtos;

public record UserDto
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenReading)]
    public required string Id { get; set; }

    public string? Email { get; set; }
    public string? UserName { get; set; }
    public string? PhoneNumber { get; set; }
}