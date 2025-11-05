using BlogApi.Domain;
using BlogApi.Services.Interfaces;
using BlogApi.Utilities;

namespace BlogApi.Services;

public class BlogService(ILogger<BlogService> logger, IBlogRepository repository) : IBlogService
{
    private readonly ILogger<BlogService> _logger = logger;
    private readonly IBlogRepository _repository = repository;

    public async Task<CommentDto?> CreateComment(string userId, int postId, CommentDto commentDto)
    {
        _logger.LogDebug("BlogService: Creating comment form arguments.");
        var userModel = await _repository.GetUserModelAsync(userId);
        var postModel = await _repository.GetPostModelAsync(postId);

        if (postModel is null || userModel is null)
        {
            _logger.LogWarning(
                "BlogService: Can't create a comment model: Post or User doesn't exist."
            );
            return null;
        }

        CommentModel commentModel = new()
        {
            User = userModel,
            Post = postModel,
            Content = commentDto.Content,
            CreatedAt = commentDto.CreatedAt,
        };
        var createdModel = await _repository.CreateCommentModel(postModel, commentModel);
        return Utils.CommentModel2Dto(createdModel);
    }

    Task<LikeModel> IBlogService.CreateLike(string userId, int postId, LikeDto likeDto)
    {
        throw new NotImplementedException();
    }

    Task<PostDto> IBlogService.CreatePost(string userId, PostDto postDto)
    {
        throw new NotImplementedException();
    }

    Task IBlogService.DeleteComment(int commentId)
    {
        throw new NotImplementedException();
    }

    Task IBlogService.DeleteLike(int likeId)
    {
        throw new NotImplementedException();
    }

    Task IBlogService.DeletePost(int postId)
    {
        throw new NotImplementedException();
    }

    Task IBlogService.DeleteUser(string userId)
    {
        throw new NotImplementedException();
    }

    Task<CommentDto?> IBlogService.GetCommentAsync(int commentId)
    {
        throw new NotImplementedException();
    }

    Task<PostDto?> IBlogService.GetPostAsync(int postId)
    {
        throw new NotImplementedException();
    }

    Task<CommentDto> IBlogService.UpdateComment(CommentDto commentDto)
    {
        throw new NotImplementedException();
    }

    Task<PostDto> IBlogService.UpdatePost(PostDto postDto)
    {
        throw new NotImplementedException();
    }

    Task<UserDto> IBlogService.UpdateUser(UserDto userDto)
    {
        throw new NotImplementedException();
    }
}
