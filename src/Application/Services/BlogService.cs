using BlogApi.Application.Dtos;
using BlogApi.Application.Services.Interfaces;
using BlogApi.Domain.Entities;
using BlogApi.Domain.Interfaces;
using BlogApi.Utilities;

namespace BlogApi.Application.Services;

public class BlogService(ILogger<BlogService> logger, IBlogRepository repository) : IBlogService
{
    private readonly ILogger<BlogService> _logger = logger;
    private readonly IBlogRepository _repository = repository;

    public async Task<ValueTuple<int, PostDto>?> CreatePost(string userId, PostDto postDto)
    {
        _logger.LogDebug("BlogService: Creating post.");
        var userModel = await _repository.GetUserModelAsync(userId);

        if (userModel is null)
        {
            _logger.LogDebug("BlogService: Can't create the post model: User doesn't exist.");
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
        return (createdModel.Id, Utils.PostModel2Dto(createdModel));
    }

    public async Task<ValueTuple<int, CommentDto>?> CreateComment(
        string userId,
        int postId,
        CommentDto commentDto
    )
    {
        _logger.LogDebug("BlogService: Creating comment.");
        var userModel = await _repository.GetUserModelAsync(userId);
        var postModel = await _repository.GetPostModelAsync(postId);

        if (postModel is null || userModel is null)
        {
            _logger.LogDebug(
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
        return (createdModel.Id, Utils.CommentModel2Dto(createdModel));
    }

    public async Task<ValueTuple<int, LikeDto>?> CreateLike(
        string userId,
        int postId,
        LikeDto likeDto
    )
    {
        _logger.LogDebug("BlogService: Creating like.");
        var userModel = await _repository.GetUserModelAsync(userId);
        var postModel = await _repository.GetPostModelAsync(postId);

        if (postModel is null || userModel is null)
        {
            _logger.LogDebug(
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
        return (createdModel.Id, Utils.LikeModel2Dto(createdModel));
    }

    public async Task<PostDto?> GetPostAsync(int postId)
    {
        _logger.LogDebug("BlogService: Retrieving post.");
        var model = await _repository.GetPostModelAsync(postId);

        if (model is null)
        {
            _logger.LogDebug("BlogService: Post doesn't exist.");
            return null;
        }

        return Utils.PostModel2Dto(model);
    }

    public async Task<CommentDto?> GetCommentAsync(int commentId)
    {
        _logger.LogDebug("BlogService: Retrieving comment.");
        var model = await _repository.GetCommentModelAsync(commentId);

        if (model is null)
        {
            _logger.LogDebug("BlogService: Comment doesn't exist.");
            return null;
        }

        return Utils.CommentModel2Dto(model);
    }

    public async Task<IEnumerable<CommentDto>?> GetCommentsFromPostAsync(int postId)
    {
        _logger.LogDebug("BlogService: Retrieving Comments.");
        var model = await _repository.GetPostModelAsync(postId);

        if (model is null)
        {
            _logger.LogDebug("BlogService: Post doesn't exist.");
            return null;
        }

        return [.. model.CommentModelNavigation.Select(Utils.CommentModel2Dto)];
    }

    public async Task<LikeDto?> GetLikeAsync(int likeId)
    {
        _logger.LogDebug("BlogService: Retrieving Like.");
        var model = await _repository.GetLikeModelAsync(likeId);

        if (model is null)
        {
            _logger.LogDebug("BlogService: Like doesn't exist.");
            return null;
        }

        return Utils.LikeModel2Dto(model);
    }

    public async Task<IEnumerable<LikeDto>?> GetLikesFromPostAsync(int postId)
    {
        _logger.LogDebug("BlogService: Retrieving Likes.");
        var model = await _repository.GetPostModelAsync(postId);

        if (model is null)
        {
            _logger.LogDebug("BlogService: Post doesn't exist.");
            return null;
        }

        return [.. model.LikeModelNavigation.Select(Utils.LikeModel2Dto)];
    }

    public async Task<PostDto?> UpdatePost(int postId, PostDto postDto)
    {
        _logger.LogDebug("BlogService: Editing post.");
        var postModel = await _repository.FindPostModelById(postId);

        if (postModel is null)
        {
            _logger.LogDebug("BlogService: Post doesn't exist.");
            return null;
        }
        else
        {
            postModel.Title = postDto.Title;
            postModel.Content = postDto.Content;
            postModel.CreatedAt = postDto.CreatedAt;
            postModel.UpdatedAt = postDto.UpdatedAt;
            var returnedModel = await _repository.UpdatePostModel(postModel);
            return Utils.PostModel2Dto(returnedModel);
        }
    }

    public async Task<CommentDto?> UpdateComment(int commentId, CommentDto commentDto)
    {
        _logger.LogDebug("BlogService: Editing comment.");
        var commentModel = await _repository.FindCommentModelById(commentId);

        if (commentModel is null)
        {
            _logger.LogDebug("BlogService: Comment doesn't exist.");
            return null;
        }
        else
        {
            commentModel.Content = commentDto.Content;
            commentModel.CreatedAt = commentDto.CreatedAt;
            var returnedModel = await _repository.UpdateCommentModel(commentModel);
            return Utils.CommentModel2Dto(returnedModel);
        }
    }

    public async Task DeletePost(int postId)
    {
        _logger.LogDebug("BlogService: Removing post.");
        var model = await _repository.FindPostModelById(postId);

        if (model is null)
            _logger.LogDebug("BlogService: Post doesn't exist.");
        else
            await _repository.DeletePostModel(model);
    }

    public async Task DeleteComment(int commentId)
    {
        _logger.LogDebug("BlogService: Removing comment.");
        var model = await _repository.FindCommentModelById(commentId);

        if (model is null)
            _logger.LogDebug("BlogService: Comment doesn't exist.");
        else
            await _repository.DeleteCommentModel(model);
    }

    public async Task DeleteLike(int likeId)
    {
        _logger.LogDebug("BlogService: Removing like.");
        var model = await _repository.FindLikeModelById(likeId);

        if (model is null)
            _logger.LogDebug("BlogService: Like doesn't exist.");
        else
            await _repository.DeleteLikeModel(model);
    }

    public async Task DeleteUser(string userId)
    {
        _logger.LogDebug("BlogService: Removing user.");
        var model = await _repository.FindUserModelById(userId);

        if (model is null)
            _logger.LogDebug("BlogService: User doesn't exist.");
        else
            await _repository.DeleteUserModel(model);
    }
}
