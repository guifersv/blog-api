using BlogApi.Application.Dtos;
using BlogApi.Domain.Entities;

namespace BlogApi.Utilities;

public static class Utils
{
    public static CommentDto CommentModel2Dto(CommentModel commentModel)
    {
        return new()
        {
            Id = commentModel.Id,
            Content = commentModel.Content,
            CreatedAt = commentModel.CreatedAt,
        };
    }

    public static LikeDto LikeModel2Dto(LikeModel likeModel)
    {
        return new() { Id = likeModel.Id, CreatedAt = likeModel.CreatedAt };
    }

    public static PostDto PostModel2Dto(PostModel postModel)
    {
        return new()
        {
            Id = postModel.Id,
            Title = postModel.Title,
            Content = postModel.Content,
            CreatedAt = postModel.CreatedAt,
            UpdatedAt = postModel.UpdatedAt,
        };
    }

    public static UserDto UserModel2Dto(UserModel userModel)
    {
        return new()
        {
            Id = userModel.Id,
            Email = userModel.Email,
            UserName = userModel.UserName,
            PhoneNumber = userModel.PhoneNumber,
        };
    }
}
