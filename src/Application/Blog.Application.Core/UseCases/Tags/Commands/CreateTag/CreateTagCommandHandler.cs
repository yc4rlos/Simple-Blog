using Blog.Application.Core.Data;
using Blog.Application.Core.Helpers;
using Blog.Application.Core.UseCases.Shared.Exceptions;
using Blog.Application.Core.UseCases.Shared.Exceptions.Validation;
using Blog.Application.Core.Validators.CustomFluentValidations;
using Blog.Application.Core.Validators.SlugValidator;
using Blog.Domain.Abstractions.Services;
using Blog.Domain.Models;

namespace Blog.Application.Core.UseCases.Tags.Commands.CreateTag;

internal class CreateTagCommandHandler(
    IApplicationDbContext dbContext,
    IFileService fileService,
    ISlugValidator slugValidator)
    : IRequestHandler<CreateTagCommand>
{
    public async Task Handle(CreateTagCommand command, CancellationToken cancellationToken)
    {
        // Image
        await using var stream = command.Image.OpenReadStream();
        var imageName = await fileService.AddFileAsync(command.Image.Name, stream);

        var tag = new Tag
        {
            Name = command.Name,
            Slug = SlugHelper.CreateSlug(command.Name),
            ImageFileName = imageName
        };

        // CheckSlug
        var slugAlreadyRegistered = await slugValidator.SlugExistsAsync(tag, cancellationToken);
        if (slugAlreadyRegistered)
        {
            throw new SlugAlreadyRegisteredException(tag.Slug);
        }

        dbContext.Tags.Add(tag);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}