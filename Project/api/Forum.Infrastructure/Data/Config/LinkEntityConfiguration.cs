using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Forum.Core;
using Forum.Core.Aggregates.Link.Entities;

namespace Forum.Infrastructure.Data.Config;

public class LinkEntityConfiguration : BaseEntityConfiguration<LinkEntity>
{
    protected override void ConfigureEntity(EntityTypeBuilder<LinkEntity> builder)
    {
        builder.ToTable("Link");

        builder.Property(x => x.FileId)
            .IsRequired();

        builder.Property(x => x.OneTime)
            .IsRequired();

        builder.Property(x => x.ExpiryDate);

        builder.Property(x => x.Password);

        builder.Property(x => x.Salt)
            .HasColumnType("varbinary(Max)");


    }
}
