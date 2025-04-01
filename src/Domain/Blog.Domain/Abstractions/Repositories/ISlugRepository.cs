using System;
using Blog.Domain.Abstractions.Entities;

namespace Blog.Domain.Abstractions.Repositories;

public interface ISlugRepository
{
    Task<bool> ExistsAsync<T>(T entity, CancellationToken cancellationToken = default)
        where T : class, ISlug;
}
