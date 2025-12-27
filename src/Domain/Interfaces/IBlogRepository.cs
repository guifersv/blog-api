using BlogApi.Domain.Entities;

namespace BlogApi.Domain.Interfaces;

public interface IBlogRepository
{
    public Task<PostModel> CreatePostModel(UserModel userModel, PostModel postModel);
    public Task<CommentModel> CreateCommentModel(PostModel postModel, CommentModel commentModel);
    public Task<LikeModel> CreateLikeModel(PostModel postModel, LikeModel likeModel);

    public Task<PostModel?> FindPostModelById(int postModelId);
    public Task<PostModel?> GetPostModelAsync(int postModelId);
    public Task<CommentModel?> FindCommentModelById(int commentModelId);
    public Task<CommentModel?> GetCommentModelAsync(int commentModelId);
    public Task<UserModel?> FindUserModelById(string userId);
    public Task<UserModel?> GetUserModelAsync(string userId);
    public Task<LikeModel?> FindLikeModelById(int likeModelId);
    public Task<LikeModel?> GetLikeModelAsync(int likeModelId);
    public Task<IEnumerable<CommentModel>?> GetCommentsFromPostAsync(int postId);
    public Task<IEnumerable<LikeModel>?> GetLikesFromPostAsync(int postId);

    public Task UpdatePostModel(PostModel postModel);
    public Task UpdateUserModel(UserModel userModel);
    public Task UpdateCommentModel(CommentModel commentModel);

    public Task DeletePostModel(PostModel postModel);
    public Task DeleteCommentModel(CommentModel commentModel);
    public Task DeleteLikeModel(LikeModel likeModel);
    public Task DeleteUserModel(UserModel userModel);
}
