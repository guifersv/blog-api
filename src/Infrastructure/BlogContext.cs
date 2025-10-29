using BlogApi.Domain;

using Microsoft.EntityFrameworkCore;

namespace BlogApi.Infrastructure;

public class BlogContext(DbContextOptions<BlogContext> options) : DbContext(options)
{
    public DbSet<UserModel> Users { get; set; }
    public DbSet<PostModel> Posts { get; set; }
    public DbSet<LikeModel> Likes { get; set; }
    public DbSet<CommentModel> Comments { get; set; }
}