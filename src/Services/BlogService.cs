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
        _logger.LogInformation("BlogService: Creating comment.");
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

    public async Task<LikeDto?> CreateLike(string userId, int postId, LikeDto likeDto)
    {
        _logger.LogInformation("BlogService: Creating like.");
        var userModel = await _repository.GetUserModelAsync(userId);
        var postModel = await _repository.GetPostModelAsync(postId);

        if (postModel is null || userModel is null)
        {
            _logger.LogWarning(
                "BlogService: Can't create the like model: Post or User doesn't exist."
            );
            return null;
        }

        LikeModel likeModel = new()
        {
            User = userModel,
            Post = postModel,
            CreatedAt = likeDto.CreatedAt,
        };

        var createdModel = await _repository.CreateLikeModel(postModel, likeModel);
        return Utils.LikeModel2Dto(createdModel);
    }

    public async Task<PostDto?> CreatePost(string userId, PostDto postDto)
    {
        _logger.LogInformation("BlogService: Creating post.");
        var userModel = await _repository.GetUserModelAsync(userId);

        if (userModel is null)
        {
            _logger.LogWarning(
                "BlogService: Can't create the post model: User with id: {userId} doesn't exist.",
                userId
            );
            return null;
        }

        PostModel postModel = new()
        {
            User = userModel,
            Title = postDto.Title,
            Content = postDto.Content,
            CreatedAt = postDto.CreatedAt,
            UpdatedAt = postDto.UpdatedAt,
        };

        var createdModel = await _repository.CreatePostModel(userModel, postModel);
        return Utils.PostModel2Dto(createdModel);
    }

    public async Task<CommentDto?> GetCommentAsync(int commentId)
    {
        _logger.LogInformation("BlogService: Retrieving comment.");
        var model = await _repository.GetCommentModelAsync(commentId);

        if (model is null)
        {
            _logger.LogWarning(
                "BlogService: Comment with id: {commentId} doesn't exist.",
                commentId
            );
            return null;
        }

        return Utils.CommentModel2Dto(model);
    }

    public async Task<PostDto?> GetPostAsync(int postId)
    {
        _logger.LogInformation("BlogService: Retrieving post.");
        var model = await _repository.GetPostModelAsync(postId);

        if (model is null)
        {
            _logger.LogWarning("BlogService: Post with id: {postId} doesn't exist.", postId);
            return null;
        }

        return Utils.PostModel2Dto(model);
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
