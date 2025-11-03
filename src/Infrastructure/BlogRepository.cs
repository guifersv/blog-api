using BlogApi.Domain;
using BlogApi.Services.Interfaces;

namespace BlogApi.Infrastructure;

public class BlogRepository(BlogContext context) : IBlogRepository
{
    private readonly BlogContext _context = context;

    public async Task<PostModel> CreatePostModel(PostModel postModel)
    {
        var createdModel = await _context.Posts.AddAsync(postModel);
        await _context.SaveChangesAsync();
        return createdModel.Entity;
    }

    public async Task<UserModel?> FindUserModelById(int userModelId)
    {
        return await _context.Users.FindAsync(userModelId);
    }

    public async Task<PostModel?> FindPostModelById(int postModelId)
    {
        return await _context.Posts.FindAsync(postModelId);
    }

    public async Task<CommentModel?> FindCommentModelById(int commentModel)
    {
        return await _context.Comments.FindAsync(commentModel);
    }

    public async Task UpdatePostModel(PostModel postModel)
    {
        _context.Posts.Update(postModel);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCommentModel(CommentModel commentModel)
    {
        _context.Comments.Update(commentModel);
        await _context.SaveChangesAsync();
    }

    public async Task DeletePostModel(PostModel postModel)
    {
        _context.Posts.Remove(postModel);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCommentModel(CommentModel commentModel)
    {
        _context.Comments.Remove(commentModel);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteLikeModel(LikeModel likeModel)
    {
        _context.Likes.Remove(likeModel);
        await _context.SaveChangesAsync();
    }
}