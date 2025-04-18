using Blog.Application.Core.Data;
using Blog.Application.Core.UseCases.Shared.Exceptions.User;
using Blog.Application.Core.UseCases.Shared.Exceptions.Validation;
using Blog.Domain.Abstractions.Repositories;
using Blog.Domain.Abstractions.Services;

namespace Blog.Application.Core.UseCases.User.Commands.CreateUser;

internal class CreateUserCommandHandler(
    IApplicationDbContext dbContext,
    ISlugRepository slugRepository,
    IUserService userService,
    IFileService fileService,
    IEmailService emailService)
    : IRequestHandler<CreateUserCommand>
{
    public async Task Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var user = command.ToEntity();
        var firstPassword = Guid.NewGuid().ToString("N");

        user.Password = BCrypt.Net.BCrypt.HashPassword(firstPassword);
        user.ResetPassword = true;

        if (command.Image != null)
        {
            var imageName = await fileService.AddFileAsync(command.Image.FileName, command.Image.Content);
            await command.Image.Content.DisposeAsync();
            user.ImageFileName = imageName;
        }

        // Check Email
        var emailAlreadyRegistered = await userService.EmailAlreadyExists(command.Email, cancellationToken);
        if (emailAlreadyRegistered)
        {
            throw new UserEmailAlreadyRegisteredException(command.Email);
        }

        // Check Slug
        var slugAlreadyExists = await slugRepository.ExistsAsync(user, cancellationToken);
        if (slugAlreadyExists)
        {
            throw new SlugAlreadyRegisteredException(user.Slug);
        }

        dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync(cancellationToken);

        await emailService.SendNewUserMailAsync(user, firstPassword);
    }
}