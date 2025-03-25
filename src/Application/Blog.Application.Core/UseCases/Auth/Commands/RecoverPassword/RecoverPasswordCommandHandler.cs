using Blog.Application.Core.Data;
using Blog.Application.Core.UseCases.Shared.Exceptions.User;
using Blog.Domain.Abstractions.Services;

namespace Blog.Application.Core.UseCases.Auth.Commands.RecoverPassword;

internal class RecoverPasswordCommandHandler(IApplicationDbContext dbContext, IEmailService emailService)
    : IRequestHandler<RecoverPasswordCommand>
{
    public async Task Handle(RecoverPasswordCommand notification, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == notification.Email, cancellationToken);
        
        if(user == null)
            throw new UserNotFoundException(notification.Email);

        var temporaryPassword = Guid.NewGuid().ToString("N");
        
        user.ResetPassword = true;
        user.Password = BCrypt.Net.BCrypt.HashPassword(temporaryPassword);
        
        dbContext.Users.Update(user);
        await dbContext.SaveChangesAsync(cancellationToken);
        
        await emailService.SendTemporaryPasswordMailAsync(user, temporaryPassword);
    }
}