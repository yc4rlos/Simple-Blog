using Blog.Application.Core.Data;
using Blog.Application.Core.UseCases.Shared.Exceptions.Tag;
using Blog.Application.Core.UseCases.Shared.Exceptions.Validation;
using Blog.Domain.Abstractions.Repositories;
using Blog.Domain.Abstractions.Services;

namespace Blog.Application.Core.UseCases.Tags.Commands.UpdateTag;

internal class UpdateTagCommandHandler(
    IApplicationDbContext dbContext,
    IFileService fileService,
    ISlugRepository slugValidator)
    : IRequestHandler<UpdateTagCommand>
{
    public async Task Handle(UpdateTagCommand command, CancellationToken cancellationToken)
    {
        var tag = await dbContext.Tags.FindAsync([command.Id], cancellationToken);

        if (tag == null)
            throw new TagNotFoundException(command.Id);

        // Image
        if (command.Image != null)
        {
            if (tag.ImageFileName != null)
            {
                await fileService.DeleteFileAsync(tag.ImageFileName, cancellationToken);
            }

            await using var stream = command.Image.OpenReadStream();
            tag.ImageFileName = await fileService.AddFileAsync(command.Image.Name, stream);
        }

        // Name
        if (tag.Name != command.Name)
        {
            tag.Name = command.Name;
            tag.Slug = SlugHelper.CreateSlug(command.Name);

            // Check slug
            var slugAlreadyRegistered = await slugValidator.ExistsAsync(tag, cancellationToken);
            if (slugAlreadyRegistered)
                throw new SlugAlreadyRegisteredException(tag.Slug);
        }

        dbContext.Tags.Update(tag);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}