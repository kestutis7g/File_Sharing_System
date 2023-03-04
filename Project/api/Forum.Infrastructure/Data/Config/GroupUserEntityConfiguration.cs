using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Forum.Core;
using Forum.Core.Aggregates.GroupUser.Entities;
using Ardalis.Specification;

namespace Forum.Infrastructure.Data.Config;

public class GroupUserEntityConfiguration : IEntityTypeConfiguration<GroupUserEntity>
{
    public void Configure(EntityTypeBuilder<GroupUserEntity> builder)
    {
        builder.ToTable("GroupUser");
        builder.HasKey(x => new { x.GroupId, x.UserId });

        builder.HasOne(x => x.Group)
                .WithMany()
                .HasForeignKey(x => x.GroupId)
                .IsRequired();
                //.OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .IsRequired();
                //.OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.IsAdmin);

    }
}


