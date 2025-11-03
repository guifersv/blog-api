using Microsoft.AspNetCore.Identity;

namespace BlogApi.Domain;

public class UserModel : IdentityUser
{
    public ICollection<PostModel> PostModelNavigation { get; set; } = new List<PostModel>();
    public ICollection<LikeModel> LikeModelNavigation { get; set; } = new List<LikeModel>();
    public ICollection<CommentModel> CommentModelNavigation { get; set; } = new List<CommentModel>();
}

public record UserDto
{
    public string? Email { get; set; }
    public string? UserName { get; set; }
    public string? PhoneNumber { get; set; }

    public List<PostDto> Posts { get; set; } = [];
    public List<LikeModel> Likes { get; set; } = [];
    public List<CommentModel> Comments { get; set; } = [];
}