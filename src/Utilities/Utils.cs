using BlogApi.Domain;

namespace BlogApi.Utilities;

public static class Utils
{
    public static CommentDto CommentModel2Dto(CommentModel commentModel)
    {
        CommentDto commentDto = new()
        {
            Content = commentModel.Content,
            CreatedAt = commentModel.CreatedAt,
        };
        return commentDto;
    }

    public static LikeDto LikeModel2Dto(LikeModel likeModel)
    {
        LikeDto likeDto = new() { CreatedAt = likeModel.CreatedAt };
        return likeDto;
    }

    public static PostDto PostModel2Dto(PostModel postModel)
    {
        PostDto postDto = new()
        {
            Title = postModel.Title,
            Content = postModel.Content,
            CreatedAt = postModel.CreatedAt,
            UpdatedAt = postModel.UpdatedAt,
        };
        return postDto;
    }
}
