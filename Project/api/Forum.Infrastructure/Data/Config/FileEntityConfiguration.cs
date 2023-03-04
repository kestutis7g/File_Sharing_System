using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Forum.Core;
using Forum.Core.Aggregates.File.Entities;

namespace Forum.Infrastructure.Data.Config;

public class FileEntityConfiguration : BaseEntityConfiguration<FileEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<FileEntity> builder)
    {
        builder.ToTable("File");

        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnType("nvarchar(256)");

        builder.Property(x => x.Size)
            .IsRequired()
            .HasColumnType("int");

        builder.Property(x => x.FileMime)
            .IsRequired()
            .HasColumnType("nvarchar(256)");

        builder.Property(x => x.Location)
            .IsRequired()
            .HasColumnType("nvarchar(256)");

        builder.Property(x => x.Visibility)
            .IsRequired()
            .HasColumnType("nvarchar(256)");

        builder.Property(x => x.FileBinary)
            .IsRequired()
            .HasColumnType("varbinary(Max)");



    }
}
