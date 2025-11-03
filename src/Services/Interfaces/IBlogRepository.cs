using BlogApi.Domain;

namespace BlogApi.Services.Interfaces;

public interface IBlogRepository
{
    public Task<PostModel> CreatePostModel(PostModel postModel);

    public Task<UserModel?> FindUserModelById(int userModelId);
    public Task<PostModel?> FindPostModelById(int postModelId);
    public Task<CommentModel?> FindCommentModelById(int commentModelId);

    public Task UpdatePostModel(PostModel postModel);
    public Task UpdateCommentModel(CommentModel commentModel);

    public Task DeletePostModel(PostModel postModel);
    public Task DeleteCommentModel(CommentModel commentModel);
    public Task DeleteLikeModel(LikeModel likeModel);
}