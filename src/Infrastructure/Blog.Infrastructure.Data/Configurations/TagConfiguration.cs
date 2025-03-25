using Blog.Domain.Models;

namespace Blog.Infrastructure.Data.Configurations;

public class TagConfiguration: IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.Property(x => x.ImageFileName)
            .IsRequired()
            .HasMaxLength(150);
        
        builder.HasIndex(x => x.Slug)
            .IsUnique();

        builder.HasMany(x => x.Posts)
            .WithOne(x => x.Tag);
    }
}