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
            .HasColumnType("nvarchar(Max)");

        builder.Property(x => x.Salt)
            .HasColumnType("varbinary(Max)");

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

        builder.Property(x => x.ProfilePicture)
            .HasColumnType("varbinary(Max)");

        builder.Property(x => x.FileMime);

        builder.Property(x => x.Description)
            .HasMaxLength(1024)
            .HasColumnType("nvarchar(32)");

        builder.Property(x => x.Deleted).IsRequired().HasDefaultValue(false);
        builder.Property(x => x.Banned).IsRequired().HasDefaultValue(false);

        byte[] adminSalt = { 200, 36, 211, 3, 161, 23, 161, 179, 87, 195, 33, 54, 84, 55, 28, 98 };
        byte[] sigitasSalt = { 242, 255, 172, 76, 123, 74, 148, 61, 240, 117, 9, 69, 121, 40, 87, 213 };
        builder.HasData(
                new UserEntity { Id = Guid.NewGuid(), Login = "Admin", Password = "8s8MyiZeo4bd1uvG3V+s2plWJoCb9T8mbYttaqSPrvo=", Salt = adminSalt, Role = "Admin", Name = "Admin", Lastname = "Admin", Email = "admin@admin.com", Deleted = false, Banned = false},
                new UserEntity { Id = Guid.NewGuid(), Login = "Sigitas", Password = "GBiv54yeSIos8oswXxODHti7pCZMaf0WpsYNz25skoA=", Salt = sigitasSalt, Role = "User", Name = "Sigitas", Lastname = "Sigitavicius", Email = "sigitas@gmail.com", Deleted = false, Banned = false }
                );

    }
}
