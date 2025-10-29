namespace BlogApi.Domain;

public class CommentModel
{
    public int Id { get; set; }
    public int UserModelId { get; set; }
    public int PostModelId { get; set; }

    public string? Content { get; set; }

    public DateTime CreatedAt { get; set; }
}
