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

    public async Task<PostDto?> CreatePost(string userId, PostDto postDto)
    {
        _logger.LogDebug("BlogService: Creating post.");
        var userModel = await _repository.FindUserModelById(userId);

        if (userModel is null)
        {
            _logger.LogDebug("BlogService: Can't create the post model: User doesn't exist.");
            return null;
        }

        PostModel postModel = new()
        {
            UserModelId = userId,
            User = userModel,
            Title = postDto.Title,
            Content = postDto.Content,
            CreatedAt = postDto.CreatedAt,
            UpdatedAt = postDto.UpdatedAt,
        };

        var createdModel = await _repository.CreatePostModel(userModel, postModel);
        return Utils.PostModel2Dto(createdModel);
    }

    public async Task<CommentDto?> CreateComment(string userId, int postId, CommentDto commentDto)
    {
        _logger.LogDebug("BlogService: Creating comment.");
        var userModel = await _repository.FindUserModelById(userId);
        var postModel = await _repository.FindPostModelById(postId);

        if (postModel is null || userModel is null)
        {
            _logger.LogDebug(
                "BlogService: Can't create a comment model: Post or User doesn't exist."
            );
            return null;
        }

        CommentModel commentModel = new()
        {
            UserModelId = userId,
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
        _logger.LogDebug("BlogService: Creating like.");
        var userModel = await _repository.FindUserModelById(userId);
        var postModel = await _repository.FindPostModelById(postId);

        if (postModel is null || userModel is null)
        {
            _logger.LogDebug(
                "BlogService: Can't create the like model: Post or User doesn't exist."
            );
            return null;
        }

        LikeModel likeModel = new()
        {
            UserModelId = userId,
            User = userModel,
            Post = postModel,
            CreatedAt = likeDto.CreatedAt,
        };

        var createdModel = await _repository.CreateLikeModel(postModel, likeModel);
        return Utils.LikeModel2Dto(createdModel);
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

    public async Task<IEnumerable<CommentDto>?> GetCommentsFromPostAsync(int postId)
    {
        _logger.LogDebug("BlogService: Retrieving Comments.");
        var result = await _repository.GetCommentsFromPostAsync(postId);

        if (result is null)
        {
            _logger.LogDebug("BlogService: Post doesn't exist.");
            return null;
        }

        return [.. result.Select(Utils.CommentModel2Dto)];
    }

    public async Task<IEnumerable<LikeDto>?> GetLikesFromPostAsync(int postId)
    {
        _logger.LogDebug("BlogService: Retrieving Likes.");
        var result = await _repository.GetLikesFromPostAsync(postId);

        if (result is null)
        {
            _logger.LogDebug("BlogService: Post doesn't exist.");
            return null;
        }

        return [.. result.Select(Utils.LikeModel2Dto)];
    }

    public async Task<bool> UpdatePost(string userId, int postId, PostDto postDto)
    {
        _logger.LogDebug("BlogService: Editing post.");
        var postModel = await _repository.FindPostModelById(postId);

        if (postModel is null)
        {
            _logger.LogDebug("BlogService: Post doesn't exist.");
            return false;
        }

        if (postModel.UserModelId != userId)
        {
            _logger.LogDebug("BlogService: Post Owner differ from provided userId.");
            return false;
        }

        postModel.Title = postDto.Title;
        postModel.Content = postDto.Content;
        postModel.CreatedAt = postDto.CreatedAt;
        postModel.UpdatedAt = postDto.UpdatedAt;

        await _repository.UpdatePostModel(postModel);
        return true;
    }

    public async Task<bool> UpdateComment(string userId, int commentId, CommentDto commentDto)
    {
        _logger.LogDebug("BlogService: Editing comment.");
        var commentModel = await _repository.FindCommentModelById(commentId);

        if (commentModel is null)
        {
            _logger.LogDebug("BlogService: Comment doesn't exist.");
            return false;
        }

        if (commentModel.UserModelId != userId)
        {
            _logger.LogDebug("BlogService: Comment Owner differ from provided userId.");
            return false;
        }

        commentModel.Content = commentDto.Content;
        commentModel.CreatedAt = commentDto.CreatedAt;

        await _repository.UpdateCommentModel(commentModel);
        return true;
    }

    public async Task<bool> DeletePost(string userId, int postId)
    {
        _logger.LogDebug("BlogService: Removing post.");
        var model = await _repository.FindPostModelById(postId);

        if (model is null)
        {
            _logger.LogDebug("BlogService: Post doesn't exist.");
            return false;
        }

        if (model.UserModelId != userId)
        {
            _logger.LogDebug("BlogService: Post Owner differ from provided userId.");
            return false;
        }

        await _repository.DeletePostModel(model);
        return true;
    }

    public async Task<bool> DeleteComment(string userId, int commentId)
    {
        _logger.LogDebug("BlogService: Removing comment.");
        var model = await _repository.FindCommentModelById(commentId);

        if (model is null)
        {
            _logger.LogDebug("BlogService: Comment doesn't exist.");
            return false;
        }

        if (model.UserModelId != userId)
        {
            _logger.LogDebug("BlogService: Comment Owner differ from provided userId.");
            return false;
        }

        await _repository.DeleteCommentModel(model);
        return true;
    }

    public async Task<bool> DeleteLike(string userId, int likeId)
    {
        _logger.LogDebug("BlogService: Removing like.");
        var model = await _repository.FindLikeModelById(likeId);

        if (model is null)
        {
            _logger.LogDebug("BlogService: Like doesn't exist.");
            return false;
        }

        if (model.UserModelId != userId)
        {
            _logger.LogDebug("BlogService: Like Owner differ from provided userId.");
            return false;
        }

        await _repository.DeleteLikeModel(model);
        return true;
    }

    public async Task<bool> DeleteUser(string userId)
    {
        _logger.LogDebug("BlogService: Removing user.");
        var model = await _repository.FindUserModelById(userId);

        if (model is null)
        {
            _logger.LogDebug("BlogService: User doesn't exist.");
            return false;
        }
        await _repository.DeleteUserModel(model);
        return true;
    }
}
