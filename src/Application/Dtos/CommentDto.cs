using System.Text.Json.Serialization;

namespace BlogApi.Application.Dtos;

public record CommentDto
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenReading)]
    public int Id { get; set; }
    public string? Content { get; set; }
    public DateTime CreatedAt { get; set; }
}
