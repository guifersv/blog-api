using BlogApi.Domain.Entities;

namespace BlogApi.Application.Dtos;

public record UserDto
{
    public string? Email { get; set; }
    public string? UserName { get; set; }
    public string? PhoneNumber { get; set; }

    public List<PostDto> Posts { get; set; } = [];
    public List<LikeModel> Likes { get; set; } = [];
    public List<CommentModel> Comments { get; set; } = [];
}
