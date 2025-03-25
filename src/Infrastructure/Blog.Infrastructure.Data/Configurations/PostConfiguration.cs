using Blog.Domain.Models;

namespace Blog.Infrastructure.Data.Configurations;

public class PostConfiguration: IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Title)
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(x => x.Content)
            .IsRequired();

        builder.HasIndex(x => x.Slug)
            .IsUnique();

        builder.Property(x => x.Summary)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.ImageFileName)
            .IsRequired()
            .HasMaxLength(150);

        builder.HasMany(x => x.Tags)
            .WithOne(x => x.Post);

        builder.HasOne(x => x.CreatedBy)
            .WithMany(x => x.Posts)
            .HasForeignKey(x => x.CreatedById)
            .IsRequired();

    }
}