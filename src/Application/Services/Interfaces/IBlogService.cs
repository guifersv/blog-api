using BlogApi.Application.Dtos;

namespace BlogApi.Application.Services.Interfaces;

public interface IBlogService
{
    public Task<PostDto?> CreatePost(string userId, PostDto postDto);
    public Task<CommentDto?> CreateComment(string userId, int postId, CommentDto commentDto);
    public Task<LikeDto?> CreateLike(string userId, int postId, LikeDto likeDto);

    public Task<PostDto?> GetPostAsync(int postId);
    public Task<CommentDto?> GetCommentAsync(int commentId);

    public Task<PostDto?> UpdatePost(int postId, PostDto postDto);
    public Task<CommentDto?> UpdateComment(int commentId, CommentDto commentDto);

    public Task DeletePost(int postId);
    public Task DeleteComment(int commentId);
    public Task DeleteLike(int likeId);
    public Task DeleteUser(string userId);
}