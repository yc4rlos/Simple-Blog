using System.Reflection;
using Blog.Application.Core.Data;
using Blog.Domain.Models;

namespace Blog.Infrastructure.Data.Context;

public class ApplicationDbContext: DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {}
    
    public DbSet<PostTag> PostTags { get; set; }

    public DbSet<Post> Posts { get; set; } = null!;
    public DbSet<Tag> Tags { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

}