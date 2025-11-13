using System.ComponentModel.DataAnnotations;

namespace BlogApi.Application.Dtos;

public record PostDto
{
    [StringLength(100)]
    public string? Title { get; set; }

    public string? Content { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}