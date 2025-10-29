using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;

namespace BlogApi.Domain;

public class PostModel
{
    public int Id { get; set; }

    public int UserModelId { get; set; }
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public required UserModel User { get; set; }

    [MaxLength(100)]
    public string? Title { get; set; }

    public string? Content { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public ICollection<CommentModel> CommentModelNavigation { get; set; } = new List<CommentModel>();
    public ICollection<LikeModel> LikeModelNavigation { get; set; } = new List<LikeModel>();
}