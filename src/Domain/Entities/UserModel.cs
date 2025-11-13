using Microsoft.AspNetCore.Identity;

namespace BlogApi.Domain.Entities;

public class UserModel : IdentityUser
{
    public ICollection<PostModel> PostModelNavigation { get; set; } = new List<PostModel>();
    public ICollection<LikeModel> LikeModelNavigation { get; set; } = new List<LikeModel>();
    public ICollection<CommentModel> CommentModelNavigation { get; set; } =
        new List<CommentModel>();
}
