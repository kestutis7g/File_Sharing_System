using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Forum.Core;
using Forum.Core.Aggregates.GroupPost.Entities;
using Ardalis.Specification;

namespace Forum.Infrastructure.Data.Config;

public class GroupPostEntityConfiguration : IEntityTypeConfiguration<GroupPostEntity>
{
    public void Configure(EntityTypeBuilder<GroupPostEntity> builder)
    {
        builder.ToTable("GroupPost");
        builder.HasKey(x => new { x.GroupId, x.PostId });

        builder.HasOne(x => x.Group)
                .WithMany()
                .HasForeignKey(x => x.GroupId)
                .IsRequired();
                //.OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Post)
                .WithMany()
                .HasForeignKey(x => x.PostId)
                .IsRequired();
                //.OnDelete(DeleteBehavior.Restrict);

    }
}


