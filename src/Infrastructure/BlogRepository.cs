
using BlogApi.Domain;
using BlogApi.Services.Interfaces;

namespace BlogApi.Infrastructure;

public class BlogRepository(BlogContext context) : IBlogRepository
{
    private readonly BlogContext _context = context;

    Task IBlogRepository.CreateCommentModel(CommentModel commentModel)
    {
        throw new NotImplementedException();
    }

    Task IBlogRepository.CreateLikeModel(LikeModel likeModel)
    {
        throw new NotImplementedException();
    }

    Task IBlogRepository.CreatePostModel(PostModel postModel)
    {
        throw new NotImplementedException();
    }

    Task IBlogRepository.DeleteCommentModel(CommentModel commentModel)
    {
        throw new NotImplementedException();
    }

    Task IBlogRepository.DeleteLikeModel(LikeModel likeModel)
    {
        throw new NotImplementedException();
    }

    Task IBlogRepository.DeletePostModel(PostModel postModel)
    {
        throw new NotImplementedException();
    }

    Task IBlogRepository.FindCommentModelById(int commentModelId)
    {
        throw new NotImplementedException();
    }

    Task IBlogRepository.FindPostModelById(int postModelId)
    {
        throw new NotImplementedException();
    }

    Task IBlogRepository.FindUserModelById(int userModelId)
    {
        throw new NotImplementedException();
    }

    Task IBlogRepository.UpdateCommentModel(CommentModel commentModel)
    {
        throw new NotImplementedException();
    }

    Task IBlogRepository.UpdatePostModel(PostModel postModel)
    {
        throw new NotImplementedException();
    }
}