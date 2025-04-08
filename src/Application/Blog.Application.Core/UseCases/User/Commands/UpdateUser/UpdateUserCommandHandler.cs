using Blog.Application.Core.Data;
using Blog.Application.Core.UseCases.Shared.Exceptions.User;
using Blog.Application.Core.UseCases.Shared.Exceptions.Validation;
using Blog.Domain.Abstractions.Repositories;
using Blog.Domain.Abstractions.Services;

namespace Blog.Application.Core.UseCases.User.Commands.UpdateUser;

internal class UpdateUserCommandHandler(
    IApplicationDbContext dbContext,
    ISlugRepository slugRepository,
    IUserService userService,
    IFileService fileService)
    : IRequestHandler<UpdateUserCommand>
{
    public async Task Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users.FindAsync([command.Id], cancellationToken);

        if (user == null)
            throw new UserNotFoundException(command.Id);

        var nameChanged = command.Name != user.Name;
        var emailChanged = command.Email != user.Email;

        command.ToEntity(user);

        //Check Name
        if (nameChanged)
        {
            var slugAlreadyRegistered = await slugRepository.ExistsAsync(user, cancellationToken);
            if (slugAlreadyRegistered)
            {
                throw new SlugAlreadyRegisteredException(user.Slug);
            }
        }

        // Check Email
        if (emailChanged)
        {
            var emailAlreadyRegistered = await userService.EmailAlreadyExists(user.Email, cancellationToken);
            if (emailAlreadyRegistered)
            {
                throw new UserEmailAlreadyRegisteredException(user.Email);
            }
        }

        // Updates image
        if (command.Image != null)
        {
            if (user.ImageFileName != null)
            {
                await fileService.DeleteFileAsync(user.ImageFileName, cancellationToken);
            }
            var imageName = await fileService.AddFileAsync(command.Image.FileName, command.Image.Content);
            await command.Image.Content.DisposeAsync();
            user.ImageFileName = imageName;
        }

        dbContext.Users.Update(user);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}