using Blog.Application.Core.Data;
using Blog.Application.Core.UseCases.Shared.Exceptions.Tag;
using Blog.Application.DTOs.Tag;

namespace Blog.Application.Core.UseCases.Tags.Queries.GetTag;

public class GetTagQueryHandler(IApplicationDbContext dbContext)
    : IRequestHandler<GetTagQuery, GetTagQueryResult>
{
    public async Task<GetTagQueryResult> Handle(GetTagQuery query, CancellationToken cancellationToken)
    {
        var tag = await dbContext.Tags
            .FindAsync([query.Id], cancellationToken);

        if (tag == null)
            throw new TagNotFoundException(query.Id);

        return new GetTagQueryResult(tag.ToTagDto());
    }
}