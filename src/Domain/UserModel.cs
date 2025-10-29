using System.ComponentModel.DataAnnotations;


namespace BlogApi.Domain;

public class UserModel
{
    public int Id { get; set; }

    [MaxLength(20)]
    public required string Name { get; set; }

    [EmailAddress, MaxLength(254)]
    public required string Email { get; set; }

    public required string Password { get; set; }
    public DateTime CreatedAt { get; set; }

    public ICollection<PostModel> PostModelNavigation { get; set; } = new List<PostModel>();
    public ICollection<LikeModel> LikeModelNavigation { get; set; } = new List<LikeModel>();
    public ICollection<CommentModel> CommentModelNavigation { get; set; } = new List<CommentModel>();
}