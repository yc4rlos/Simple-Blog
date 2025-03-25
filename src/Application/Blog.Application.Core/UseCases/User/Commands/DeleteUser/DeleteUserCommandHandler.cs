using Blog.Application.Core.Data;
using Blog.Application.Core.UseCases.Shared.Exceptions.User;

namespace Blog.Application.Core.UseCases.User.Commands.DeleteUser;

internal class DeleteUserCommandHandler(IApplicationDbContext dbContext)
    : IRequestHandler<DeleteUserCommand>
{
    public async Task Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users.FindAsync([command.Id], cancellationToken);

        if (user == null)
        {
            throw new UserNotFoundException(command.Id);
        }

        user.DeletedAt = DateTime.Now;

        dbContext.Users.Update(user);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}