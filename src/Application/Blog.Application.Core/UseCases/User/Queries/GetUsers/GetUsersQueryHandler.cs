using Blog.Application.Core.Data;
using Blog.Application.DTOs.User;

namespace Blog.Application.Core.UseCases.User.Queries.GetUsers;

internal class GetUsersQueryHandler(IApplicationDbContext dbContext)
    : IRequestHandler<GetUsersQuery, GetUsersQueryResult>
{
    public async Task<GetUsersQueryResult> Handle(GetUsersQuery query, CancellationToken cancellationToken)
    {
        var users = await GetUsersQuery()
            .Take(query.Quantity)
            .Skip(query.Quantity * (query.Page -1))
            .ToListAsync(cancellationToken);
        
        var totalCount = await GetUsersQuery()
            .CountAsync(cancellationToken);
        
        var lastPage = (int)Math.Ceiling(totalCount / (double)query.Quantity);

        var usersDto = users.ToUserDtos();
        
        return new GetUsersQueryResult(usersDto, totalCount, lastPage);
    }

    private IQueryable<Domain.Models.User> GetUsersQuery()
    {
        return dbContext.Users.AsNoTracking()
            .Where(x => x.DeletedAt == null)
            .AsQueryable();
    }
}