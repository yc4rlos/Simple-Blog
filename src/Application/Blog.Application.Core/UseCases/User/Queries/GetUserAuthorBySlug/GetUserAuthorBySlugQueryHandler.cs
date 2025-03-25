using Blog.Application.Core.Data;
using Blog.Application.Core.UseCases.Shared.Exceptions.User;
using Blog.Application.DTOs.User;
using Blog.Domain.Enums;

namespace Blog.Application.Core.UseCases.User.Queries.GetUserAuthorBySlug;

internal class GetUserAuthorBySlugQueryHandler(IApplicationDbContext dbContext)
    : IRequestHandler<GetUserAuthorBySlugQuery, GetUserAuthorBySlugResponse>
{
    public async Task<GetUserAuthorBySlugResponse> Handle(GetUserAuthorBySlugQuery request, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users
            .Where(u => u.Slug == request.Slug && u.Role == Role.Author)
            .Select(x => x.ToUserAuthorDto())
            .FirstOrDefaultAsync(cancellationToken);
        
        if(user == null)
            throw new AuthorNotFoundException(request.Slug);
        
        return new GetUserAuthorBySlugResponse(user);
    }
}