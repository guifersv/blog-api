using BlogApi.Application.Dtos;
using BlogApi.Domain.Entities;

namespace BlogApi.Utilities;

public static class Utils
{
    public static CommentDto CommentModel2Dto(CommentModel commentModel)
    {
        return new() { Content = commentModel.Content, CreatedAt = commentModel.CreatedAt };
    }

    public static LikeDto LikeModel2Dto(LikeModel likeModel)
    {
        return new() { CreatedAt = likeModel.CreatedAt };
    }

    public static PostDto PostModel2Dto(PostModel postModel)
    {
        return new()
        {
            Title = postModel.Title,
            Content = postModel.Content,
            CreatedAt = postModel.CreatedAt,
            UpdatedAt = postModel.UpdatedAt,
        };
    }
}
