namespace Blog.Application.Core.Data;

public interface IApplicationDbContext
{
    DbSet<Post> Posts { get;}
    DbSet<Tag> Tags { get; }
    DbSet<PostTag> PostTags { get; }
    DbSet<User> Users { get; }

    public DbSet<TEntity> Set<TEntity>() where TEntity : class;
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}