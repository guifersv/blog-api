using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlogApi.Application.Dtos;

public record PostDto
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenReading)]
    public int Id { get; set; }

    [StringLength(100)]
    public string? Title { get; set; }

    public string? Content { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
