namespace BlogApi.Domain;

public class LikeModel
{
    public int Id { get; set; }
    public int UserModelId { get; set; }
    public int PostModelId { get; set; }

    public DateTime CreatedAt { get; set; }
}