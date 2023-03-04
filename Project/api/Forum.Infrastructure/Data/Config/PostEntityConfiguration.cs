using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Forum.Core;
using Forum.Core.Aggregates.Post.Entities;

namespace Forum.Infrastructure.Data.Config;

public class PostEntityConfiguration : BaseEntityConfiguration<PostEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<PostEntity> builder)
    {
        builder.ToTable("Post");

        builder.Property(x => x.Title)
            .HasMaxLength(32)
            .HasColumnType("nvarchar(32)");

        builder.Property(x => x.Content)
            .IsRequired()
            .HasMaxLength(1024)
            .HasColumnType("nvarchar(32)");

        builder.Property(x => x.Picture);
        builder.Property(x => x.Type).IsRequired();

        builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.Deleted).IsRequired().HasDefaultValue(false);
        builder.Property(x => x.Banned).IsRequired().HasDefaultValue(false);

    }
}
