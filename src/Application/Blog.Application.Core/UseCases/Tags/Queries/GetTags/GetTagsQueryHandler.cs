using Blog.Application.Core.Data;
using Blog.Application.DTOs.Tag;
using Blog.Domain.Models;

namespace Blog.Application.Core.UseCases.Tags.Queries.GetTags;

internal class GetTagsQueryHandler(IApplicationDbContext dbContext) 
    : IRequestHandler<GetTagsQuery, GetTagsResult>
{
    public async Task<GetTagsResult> Handle(GetTagsQuery query, CancellationToken cancellationToken)
    {
        var totalCount = await GetDbQueryTag(query).CountAsync(cancellationToken);

        if (totalCount == 0)
        {
            return new GetTagsResult(new List<TagDto>(), 0, 0);
        }

        var tags = await GetDbQueryTag(query)
            .Take(query.Quantity)
            .Select(x => x.ToTagDto())
            .ToListAsync(cancellationToken);

        tags = tags.OrderByDescending(x => x.PostCount)
            .ToList();

        var lastPage = (int)Math.Ceiling(totalCount / (double)query.Quantity);

        return new GetTagsResult(tags, totalCount, lastPage);
    }

    private IQueryable<Tag> GetDbQueryTag(GetTagsQuery query)
    {
        var result = dbContext.Tags
            .AsNoTracking()
            .Include(x => x.Posts
                .Where(p => !query.CountOnlyPostedPosts || p.Post.PostDate < DateTime.Now))
            .Where(x => x.DeletedAt == null)
            .AsQueryable();

        if (!string.IsNullOrEmpty(query.SearchTerm))
        {
            var searchTerm = query.SearchTerm.ToLower().Trim();
            result = result.Where(x => x.Name.ToLower().Contains(searchTerm));
        }

        if (!query.IncludeEmpty)
        {
            result = result.Where(x => x.Posts.Any());
        }

        return result;
    }
}