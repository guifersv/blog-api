using BlogApi.Domain;

namespace BlogApi.Services.Interfaces;

public interface IBlogRepository
{
    public Task CreatePostModel(PostModel postModel);
    public Task CreateCommentModel(CommentModel commentModel);
    public Task CreateLikeModel(LikeModel likeModel);

    public Task FindUserModelById(int userModelId);
    public Task FindPostModelById(int postModelId);
    public Task FindCommentModelById(int commentModelId);

    public Task UpdatePostModel(PostModel postModel);
    public Task UpdateCommentModel(CommentModel commentModel);

    public Task DeletePostModel(PostModel postModel);
    public Task DeleteCommentModel(CommentModel commentModel);
    public Task DeleteLikeModel(LikeModel likeModel);
}