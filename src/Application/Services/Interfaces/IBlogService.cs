using BlogApi.Application.Dtos;

namespace BlogApi.Application.Services.Interfaces;

public interface IBlogService
{
    public Task<ValueTuple<int, PostDto>?> CreatePost(string userId, PostDto postDto);
    public Task<ValueTuple<int, CommentDto>?> CreateComment(
        string userId,
        int postId,
        CommentDto commentDto
    );
    public Task<ValueTuple<int, LikeDto>?> CreateLike(string userId, int postId, LikeDto likeDto);

    public Task<PostDto?> GetPostAsync(int postId);
    public Task<CommentDto?> GetCommentAsync(int commentId);
    public Task<IEnumerable<CommentDto>?> GetCommentsFromPostAsync(int postId);
    public Task<LikeDto?> GetLikeAsync(int likeId);
    public Task<IEnumerable<LikeDto>?> GetLikesFromPostAsync(int postId);

    public Task<PostDto?> UpdatePost(string userId, int postId, PostDto postDto);
    public Task<CommentDto?> UpdateComment(string userId, int commentId, CommentDto commentDto);

    public Task<bool> DeletePost(string userId, int postId);
    public Task<bool> DeleteComment(string userId, int commentId);
    public Task<bool> DeleteLike(string userId, int likeId);
    public Task<bool> DeleteUser(string userId);
}
