using Blog.Application.Core.Data;
using Blog.Application.Core.UseCases.Shared.Exceptions.Tag;
using Blog.Application.DTOs.Tag;

namespace Blog.Application.Core.UseCases.Tags.Queries.GetTagBySlug;

internal class GetTagBySlugQueryHandler(IApplicationDbContext dbContext)
    : IRequestHandler<GetTagBySlugQuery, GetTagBySlugResult>
{
    public async Task<GetTagBySlugResult> Handle(GetTagBySlugQuery query, CancellationToken cancellationToken)
    {
        var tag = await dbContext.Tags
            .AsNoTracking()
            .Where(x => x.DeletedAt == null)
            .FirstOrDefaultAsync(x => x.Slug == query.Slug, cancellationToken);

        if (tag == null)
            throw new TagNotFoundException(query.Slug);

        return new GetTagBySlugResult(tag.ToTagDto());
    }
}