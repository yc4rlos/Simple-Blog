using Blog.Domain.Models;
using Blog.Infrastructure.Data.InitialData;

namespace Blog.Infrastructure.Data.Configurations;

public class UserConfiguration: IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Name)
            .HasMaxLength(150)
            .IsRequired();
        
        builder.Property(x => x.Slug)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(x => x.ImageFileName)
            .HasMaxLength(150);
        
        builder.HasIndex(x => x.Email)
            .IsUnique();

        builder.Property(x => x.About);

        builder.Property(x => x.Role)
            .IsRequired();
        
        builder.Property(x => x.Password)
            .IsRequired();

        SeedData(builder);
    }

    private void SeedData(EntityTypeBuilder<User> builder )
    {
        builder.HasData(UserInitialData.Data);
    }
}