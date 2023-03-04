

using Forum.Core.Aggregates.Comment.Entities;
using Forum.Core.Aggregates.Group.Entities;
using Forum.Core.Aggregates.GroupPost.Entities;
using Forum.Core.Aggregates.Post.Entities;
using Forum.Core.Aggregates.User.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Forum.Infrastructure.Data;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> configuration) : base(configuration)
    {

    }
    //public DbSet<CommentEntity> Comments { get; set; }
    //public DbSet<GroupEntity> Groups { get; set; }
    //public DbSet<GroupPostEntity> GroupPosts { get; set; }
    //public DbSet<PostEntity> Posts { get; set; }
    //public DbSet<UserEntity> Users { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());
    }
}
