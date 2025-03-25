using Blog.Application.Core.Data;
using Blog.Domain.Abstractions.Entities;

namespace Blog.Application.Core.Validators.SlugValidator;

public class SlugValidator(IApplicationDbContext dbContext) : ISlugValidator
{
    public Task<bool> SlugExistsAsync<T>(T entity, CancellationToken cancellationToken = default) where T : class, ISlug
    {
        return dbContext.Set<T>().AnyAsync(e => e.Slug == entity.Slug, cancellationToken);
    }
}