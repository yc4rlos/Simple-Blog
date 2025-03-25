using Blog.Application.Core.Data;
using Blog.Application.DTOs.Post;

namespace Blog.Application.Core.UseCases.Posts.Queries.GetPostsByUserSlug;

internal class GetPostsByUserQueryHandler(IApplicationDbContext dbContext)
    : IRequestHandler<GetPostsByUserQuery, GetPostsByUserResult>
{
    public async Task<GetPostsByUserResult> Handle(GetPostsByUserQuery request, CancellationToken cancellationToken)
    {
        var posts = await dbContext.Posts
            .Include(x => x.CreatedBy)
            .Where(x => x.CreatedById == request.UserId)
            .Select(x => x.ToDto())
            .Take(request.Take)
            .ToListAsync(cancellationToken);
        
        return new GetPostsByUserResult(posts);
    }
}