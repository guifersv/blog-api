using Microsoft.EntityFrameworkCore;

namespace BlogApi.Domain.Entities;

public class CommentModel
{
    public int Id { get; set; }

    public int UserModelId { get; set; }

    [DeleteBehavior(DeleteBehavior.NoAction)]
    public required UserModel User { get; set; }

    public int PostModelId { get; set; }

    [DeleteBehavior(DeleteBehavior.NoAction)]
    public required PostModel Post { get; set; }

    public string? Content { get; set; }

    public DateTime CreatedAt { get; set; }
}