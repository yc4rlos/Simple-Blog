using Blog.Application.Core.Data;
using Blog.Application.Core.UseCases.Shared.Exceptions.Tag;
using Blog.Application.DTOs.Post;

namespace Blog.Application.Core.UseCases.Posts.Queries.GetPostsByTagQuery;

internal class GetPostsByTagQueryHandler(IApplicationDbContext dbContext)
: IRequestHandler<GetPostsByTagQuery, GetPostsByTagResult>
{
    public async Task<GetPostsByTagResult> Handle(GetPostsByTagQuery query, CancellationToken cancellationToken)
    {
        var tagId = await dbContext.Tags
            .AsNoTracking()
            .Where(x => x.DeletedAt == null)
            .Where(x => x.Slug == query.TagSlug)
            .Select(x => x.Id)
            .FirstOrDefaultAsync(cancellationToken);
        
        if(tagId == 0)
            throw new TagNotFoundException(query.TagSlug);

        var posts = await dbContext.Posts
            .AsNoTracking()
            .Include(x => x.CreatedBy)
            .Where(x => x.DeletedAt == null)
            .Where(x => x.Tags.Any(y => y.TagId == tagId))
            .OrderByDescending(x => x.PostDate)
            .Select(x => x.ToDto())
            .Take(query.Quantity)
            .ToListAsync(cancellationToken);

        return new GetPostsByTagResult(posts);
    }
}