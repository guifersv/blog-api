namespace BlogApi.Application.Dtos;

public record CommentDto
{
    public string? Content { get; set; }
    public DateTime CreatedAt { get; set; }
}