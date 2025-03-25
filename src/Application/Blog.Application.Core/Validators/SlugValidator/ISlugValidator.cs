using Blog.Domain.Abstractions.Entities;

namespace Blog.Application.Core.Validators.SlugValidator;

public interface ISlugValidator
{
    Task<bool> SlugExistsAsync<T>(T entity, CancellationToken cancellationToken = default)
        where T : class, ISlug;
}