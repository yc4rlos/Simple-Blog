using Blog.Application.Core.Data;
using Blog.Application.Core.UseCases.Shared.Exceptions.User;
using Blog.Application.DTOs.User;

namespace Blog.Application.Core.UseCases.User.Queries.GetUserById;

public class GetUserByIdQueryHandler(IApplicationDbContext applicationDbContext)
    : IRequestHandler<GetUserByIdQuery, GetUserByIdResult>
{
    public async Task<GetUserByIdResult> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await applicationDbContext.Users.FindAsync([request.Id], cancellationToken);

        if (user == null)
        {
            throw new UserNotFoundException(request.Id);
        }

        var result = user.ToUserCompleteDto();

        return new GetUserByIdResult(result);
    }
}