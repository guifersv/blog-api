using System.Text.Json.Serialization;

namespace BlogApi.Application.Dtos;

public record LikeDto
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenReading)]
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
}