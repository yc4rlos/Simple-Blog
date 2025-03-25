using Blog.Application.Core.Data;
using Blog.Application.DTOs.Post;

namespace Blog.Application.Core.UseCases.Posts.Queries.GetPosts;

internal class GetPostsQueryHandler(IApplicationDbContext dbContext): IRequestHandler<GetPostsQuery, GetPostsQueryResult>
{
    public async Task<GetPostsQueryResult> Handle(GetPostsQuery query, CancellationToken cancellationToken)
    {
        var totalCount = await GetDbPostQuery(query).CountAsync(cancellationToken);
        if(totalCount == 0)
        {
            return new GetPostsQueryResult(new List<PostDto>(), 0);
        }
        
        var posts = await GetDbPostQuery(query)
            .Include(x => x.CreatedBy)
            .Include(x => x.Tags)
                .ThenInclude(x => x.Tag)
            .OrderByDescending(x => x.PostDate)
            .Select(x => x.ToDto())
            .Skip(query.Quantity * (query.Page -1))
            .Take(query.Quantity)
            .ToListAsync(cancellationToken);
        
        var lastPostPage = (int)Math.Ceiling(totalCount / (double)query.Quantity);
        
        return new GetPostsQueryResult(posts, lastPostPage);
    }

    
    private IQueryable<Post> GetDbPostQuery(GetPostsQuery query)
    {
        var result = dbContext.Posts
            .AsNoTracking()
            .Where(x => x.DeletedAt == null)
            .AsQueryable();

        if (!string.IsNullOrEmpty(query.SearchTerm))
        {
            var searchTerm = query.SearchTerm.ToLower().Trim();
            result = result.Where(x => x.Title.ToLower().Contains(searchTerm) ||
                              x.Content.ToLower().Contains(searchTerm) || 
                              x.Tags.Select(t => t.Tag.Name).Contains(searchTerm));
        }

        if (query.OnlyPosted)
        {
            result = result.Where(x => DateTime.Now >= x.PostDate);
        }

        return result;
    }
}