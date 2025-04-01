using Blog.Domain.Abstractions.Repositories;
using Blog.Infrastructure.Data.Context;

namespace Blog.Infrastructure.Persistence.Repositories;

public class SlugRepository(ApplicationDbContext dbContext) : ISlugRepository
{
    Task<bool> ISlugRepository.ExistsAsync<T>(T entity, CancellationToken cancellationToken)
    {
        return dbContext.Set<T>()
            .AnyAsync(e => e.Slug == entity.Slug, cancellationToken);
    }
}
