using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Forum.Core;
using Forum.Core.Aggregates.User.Entities;

namespace Forum.Infrastructure.Data.Config;

public class UserEntityConfiguration : BaseEntityConfiguration<UserEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable("User");
        
        builder.HasIndex(x => x.Login)
            .IsUnique();
        builder.Property(x => x.Login)
            .IsRequired()
            .HasMaxLength(32)
            .HasColumnType("nvarchar(32)");

        builder.Property(x => x.Password)
            .IsRequired()
            .HasMaxLength(32)
            .HasColumnType("nvarchar(32)");

        builder.Property(x => x.Role).IsRequired();
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(32)
            .HasColumnType("nvarchar(32)");

        builder.Property(x => x.Lastname)
            .IsRequired()
            .HasMaxLength(32)
            .HasColumnType("nvarchar(32)");

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(32)
            .HasColumnType("nvarchar(32)");

        builder.Property(x => x.Phone)
            .HasMaxLength(32)
            .HasColumnType("nvarchar(32)");

        builder.Property(x => x.ProfilePicture);
        builder.Property(x => x.FileMime);

        builder.Property(x => x.Description)
            .HasMaxLength(1024)
            .HasColumnType("nvarchar(32)");

        builder.Property(x => x.Deleted).IsRequired().HasDefaultValue(false);
        builder.Property(x => x.Banned).IsRequired().HasDefaultValue(false);

        builder.HasData(
                new UserEntity { Id = Guid.NewGuid(), Login = "Admin", Password = "Admin", Role = "Admin", Name = "Admin", Lastname = "Admin", Email = "admin@admin.com", Deleted = false, Banned = false},
                new UserEntity { Id = Guid.NewGuid(), Login = "Sigitas", Password = "Sigitas", Role = "User", Name = "Sigitas", Lastname = "Sigitavicius", Email = "sigitas@gmail.com", Deleted = false, Banned = false }
                );

    }
}
