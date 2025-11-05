using BlogApi.Domain;

namespace BlogApi.Services.Interfaces;

public interface IBlogService
{
    public Task<PostDto> CreatePost(string userId, PostDto postDto);
    public Task<CommentDto> CreateComment(string userId, int postId, CommentDto commentDto);
    public Task<LikeModel> CreateLike(string userId, int postId, LikeDto likeDto);

    public Task<PostDto?> GetPostAsync(int postId);
    public Task<CommentDto?> GetCommentAsync(int commentId);

    public Task<PostDto> UpdatePost(PostDto postDto);
    public Task<UserDto> UpdateUser(UserDto userDto);
    public Task<CommentDto> UpdateComment(CommentDto commentDto);

    public Task DeletePost(int postId);
    public Task DeleteComment(int commentId);
    public Task DeleteLike(int likeId);
    public Task DeleteUser(string userId);
}