using BlogApi.Domain;
using BlogApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Infrastructure;

public class BlogRepository(BlogContext context) : IBlogRepository
{
    private readonly BlogContext _context = context;

    public async Task<PostModel> CreatePostModel(UserModel userModel, PostModel postModel)
    {
        userModel.PostModelNavigation.Add(postModel);
        var updatedModel = await UpdateUserModel(userModel);
        await _context.SaveChangesAsync();
        return updatedModel.PostModelNavigation.Last();
    }

    public async Task<CommentModel> CreateCommentModel(
        PostModel postModel,
        CommentModel commentModel
    )
    {
        postModel.CommentModelNavigation.Add(commentModel);
        var updatedModel = await UpdatePostModel(postModel);
        return updatedModel.CommentModelNavigation.Last();
    }

    public async Task<LikeModel> CreateLikeModel(PostModel postModel, LikeModel likeModel)
    {
        postModel.LikeModelNavigation.Add(likeModel);
        var updatedModel = await UpdatePostModel(postModel);
        return updatedModel.LikeModelNavigation.Last();
    }

    public async Task<PostModel?> FindPostModelById(int postModelId)
    {
        return await _context.Posts.FindAsync(postModelId);
    }

    public async Task<PostModel?> GetPostModelAsync(int postModelId)
    {
        return await _context
            .Posts.Include(p => p.LikeModelNavigation)
            .Include(p => p.CommentModelNavigation)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == postModelId);
    }

    public async Task<CommentModel?> FindCommentModelById(int commentModelId)
    {
        return await _context.Comments.FindAsync(commentModelId);
    }

    public async Task<CommentModel?> GetCommentModelAsync(int commentModelId)
    {
        return await _context
            .Comments.AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == commentModelId);
    }

    public async Task<UserModel?> GetUserModelAsync(string userId)
    {
        return await _context.Users.AsNoTracking().FirstOrDefaultAsync(m => m.Id == userId);
    }

    public async Task<PostModel> UpdatePostModel(PostModel postModel)
    {
        var updatedModel = _context.Posts.Update(postModel);
        await _context.SaveChangesAsync();
        return updatedModel.Entity;
    }

    public async Task<UserModel> UpdateUserModel(UserModel userModel)
    {
        var updatedModel = _context.Users.Update(userModel);
        await _context.SaveChangesAsync();
        return updatedModel.Entity;
    }

    public async Task<CommentModel> UpdateCommentModel(CommentModel commentModel)
    {
        var updatedModel = _context.Comments.Update(commentModel);
        await _context.SaveChangesAsync();
        return updatedModel.Entity;
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

    public async Task DeleteUserModel(UserModel userModel)
    {
        _context.Users.Remove(userModel);
        await _context.SaveChangesAsync();
    }
}