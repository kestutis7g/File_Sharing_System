using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Forum.Core;
using Forum.Core.Aggregates.Group.Entities;

namespace Forum.Infrastructure.Data.Config;

public class GroupEntityConfiguration : BaseEntityConfiguration<GroupEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<GroupEntity> builder)
    {
        builder.ToTable("Group");

        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Visibility).IsRequired();
        builder.Property(x => x.IconPicture);
        builder.Property(x => x.BackgroundPicture);

        builder.Property(x => x.Description)
            .HasMaxLength(1024)
            .HasColumnType("nvarchar(32)");

        builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.Deleted).IsRequired().HasDefaultValue(false);
        builder.Property(x => x.Banned).IsRequired().HasDefaultValue(false);

    }
}
