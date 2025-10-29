using Microsoft.EntityFrameworkCore;

namespace BlogApi.Domain;

public class LikeModel
{
    public int Id { get; set; }

    public int UserModelId { get; set; }
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public required UserModel User { get; set; }

    public int PostModelId { get; set; }
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public required PostModel Post { get; set; }

    public DateTime CreatedAt { get; set; }
}