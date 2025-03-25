using Blog.Application.Core.Data;
using Blog.Application.Core.UseCases.Shared.Exceptions.Post;
using Blog.Application.DTOs.Post;

namespace Blog.Application.Core.UseCases.Posts.Queries.GetPost;

internal class GetPostQueryHandler(IApplicationDbContext dbContext)
    : IRequestHandler<GetPostQuery, GetPostQueryResult>
{
    public async Task<GetPostQueryResult> Handle(GetPostQuery query, CancellationToken cancellationToken)
    {
        var post = await dbContext.Posts
            .AsNoTracking()
            .Where(x => x.DeletedAt == null)
            .Include(x => x.Tags)
                .ThenInclude(x => x.Tag)
            .Include(x => x.CreatedBy)
            .FirstOrDefaultAsync(x => x.Slug == query.Slug, cancellationToken);

        if (post == null)
            throw new PostNotFoundException(query.Slug);

        var postCompleteDto = post.ToPostCompleteDto();
        
        return new GetPostQueryResult(postCompleteDto);
    }
}