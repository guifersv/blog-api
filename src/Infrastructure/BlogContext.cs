using BlogApi.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Infrastructure;

public class BlogContext(DbContextOptions<BlogContext> options)
    : IdentityDbContext<UserModel>(options)
{
    public DbSet<PostModel> Posts { get; set; }
    public DbSet<LikeModel> Likes { get; set; }
    public DbSet<CommentModel> Comments { get; set; }
}