using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Forum.Core;
using Forum.Core.Aggregates.Comment.Entities;

namespace Forum.Infrastructure.Data.Config;

public class CommentEntityConfiguration : BaseEntityConfiguration<CommentEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<CommentEntity> builder)
    {
        builder.ToTable("Comment");

        builder.HasOne(x => x.Post)
                .WithMany()
                .HasForeignKey(x => x.PostId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.ParentComment)
                .WithMany()
                .HasForeignKey(x => x.ParentId)
                .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.Content)
            .IsRequired()
            .HasMaxLength(1024)
            .HasColumnType("nvarchar(32)");

        builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.Deleted).IsRequired().HasDefaultValue(false);

    }
}
