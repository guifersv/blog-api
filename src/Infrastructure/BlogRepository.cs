using BlogApi.Domain.Entities;
using BlogApi.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Infrastructure;

public class BlogRepository(BlogContext context) : IBlogRepository
{
    private readonly BlogContext _context = context;

    public async Task<PostModel> CreatePostModel(UserModel userModel, PostModel postModel)
    {
        userModel.PostModelNavigation.Add(postModel);
        _context.Users.Update(userModel);
        await _context.SaveChangesAsync();
        return postModel;
    }

    public async Task<CommentModel> CreateCommentModel(
        PostModel postModel,
        CommentModel commentModel
    )
    {
        postModel.CommentModelNavigation.Add(commentModel);
        _context.Posts.Update(postModel);
        await _context.SaveChangesAsync();
        return commentModel;
    }

    public async Task<LikeModel> CreateLikeModel(PostModel postModel, LikeModel likeModel)
    {
        postModel.LikeModelNavigation.Add(likeModel);
        _context.Posts.Update(postModel);
        await _context.SaveChangesAsync();
        return likeModel;
    }

    public async Task<PostModel?> FindPostModelById(int postModelId)
    {
        return await _context.Posts.FindAsync(postModelId);
    }

    public async Task<PostModel?> GetPostModelAsync(int postModelId)
    {
        return await _context.Posts.AsNoTracking().FirstOrDefaultAsync(m => m.Id == postModelId);
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

    public async Task<UserModel?> FindUserModelById(string userId)
    {
        return await _context.Users.FindAsync(userId);
    }

    public async Task<UserModel?> GetUserModelAsync(string userId)
    {
        return await _context.Users.AsNoTracking().FirstOrDefaultAsync(m => m.Id == userId);
    }

    public async Task<LikeModel?> FindLikeModelById(int likeModelId)
    {
        return await _context.Likes.FindAsync(likeModelId);
    }

    public async Task<LikeModel?> GetLikeModelAsync(int likeModelId)
    {
        return await _context.Likes.AsNoTracking().FirstOrDefaultAsync(m => m.Id == likeModelId);
    }

    public async Task<IEnumerable<CommentModel>?> GetCommentsFromPostAsync(int postId)
    {
        var model = await _context
            .Posts.AsNoTracking()
            .Include(p => p.CommentModelNavigation)
            .FirstOrDefaultAsync(p => p.Id == postId);
        return model?.CommentModelNavigation;
    }

    public async Task<IEnumerable<LikeModel>?> GetLikesFromPostAsync(int postId)
    {
        var model = await _context
            .Posts.AsNoTracking()
            .Include(p => p.LikeModelNavigation)
            .FirstOrDefaultAsync(p => p.Id == postId);
        return model?.LikeModelNavigation;
    }

    public async Task UpdatePostModel(PostModel postModel)
    {
        _context.Posts.Update(postModel);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateUserModel(UserModel userModel)
    {
        _context.Users.Update(userModel);
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

    public async Task DeleteUserModel(UserModel userModel)
    {
        _context.Users.Remove(userModel);
        await _context.SaveChangesAsync();
    }
}