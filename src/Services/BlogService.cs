using BlogApi.Domain;
using BlogApi.Services.Interfaces;

namespace BlogApi.Services;

public class BlogService(ILogger<BlogService> logger, IBlogRepository repository) : IBlogService
{
    private readonly ILogger<BlogService> _logger = logger;
    private readonly IBlogRepository _repository = repository;

    Task<CommentDto> IBlogService.CreateComment(int userId, int postId, CommentDto commentDto)
    {
        throw new NotImplementedException();
    }

    Task<LikeModel> IBlogService.CreateLike(int userId, int postId, LikeDto likeDto)
    {
        throw new NotImplementedException();
    }

    Task<PostDto> IBlogService.CreatePost(int userId, PostDto postDto)
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

    Task IBlogService.DeleteUser(int userId)
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