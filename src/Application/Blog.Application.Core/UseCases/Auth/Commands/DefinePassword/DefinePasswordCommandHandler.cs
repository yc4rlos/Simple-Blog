using Blog.Application.Core.Data;
using Blog.Application.Core.UseCases.Shared.Exceptions.User;
using Blog.Domain.Abstractions.Services;

namespace Blog.Application.Core.UseCases.Auth.Commands.DefinePassword;

public class DefinePasswordCommandHandler(IApplicationDbContext dbContext, IAuthService authService)
    : IRequestHandler<DefinePasswordCommand>
{
    public async Task Handle(DefinePasswordCommand request, CancellationToken cancellationToken)
    {
        var userId = authService.GetCurrentUserId();

        if (userId is null)
            throw new UnauthorizedAccessException();
        
        var user = await dbContext.Users.FindAsync([userId], cancellationToken);
        
        if(user == null)
            throw new UserNotFoundException((int)userId);
        
        user.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
        user.ResetPassword = false;
        
        dbContext.Users.Update(user);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}