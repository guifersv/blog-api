using BlogApi.Domain;

namespace BlogApi.Services.Interfaces;

public interface IBlogRepository
{
    public Task<PostModel> CreatePostModel(UserModel userModel, PostModel postModel);
    public Task<CommentModel> CreateCommentModel(PostModel postModel, CommentModel commentModel);
    public Task<LikeModel> CreateLikeModel(PostModel postModel, LikeModel likeModel);

    public Task<PostModel?> FindPostModelById(int postModelId);
    public Task<PostModel?> GetPostModelAsync(int postModelId);
    public Task<CommentModel?> FindCommentModelById(int commentModelId);
    public Task<CommentModel?> GetCommentModelAsync(int commentModelId);
    public Task<UserModel?> GetUserModelAsync(string userId);
    public Task<UserModel?> FindUserModelById(string userId);
    public Task<LikeModel?> FindLikeModelById(int likeModelId);

    public Task<PostModel> UpdatePostModel(PostModel postModel);
    public Task<UserModel> UpdateUserModel(UserModel userModel);
    public Task<CommentModel> UpdateCommentModel(CommentModel commentModel);

    public Task DeletePostModel(PostModel postModel);
    public Task DeleteCommentModel(CommentModel commentModel);
    public Task DeleteLikeModel(LikeModel likeModel);
    public Task DeleteUserModel(UserModel userModel);
}
