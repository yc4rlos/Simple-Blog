using Blog.Domain.Models;

namespace Blog.Infrastructure.Data.Configurations;

public class PostTagConfiguration: IEntityTypeConfiguration<PostTag>
{
    public void Configure(EntityTypeBuilder<PostTag> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Tag)
            .WithMany(x => x.Posts)
            .IsRequired();

        builder.HasOne(x => x.Post)
            .WithMany(x => x.Tags)
            .IsRequired();
    }
}