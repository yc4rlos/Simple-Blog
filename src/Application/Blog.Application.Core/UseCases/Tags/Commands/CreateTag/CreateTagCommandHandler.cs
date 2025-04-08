using Blog.Application.Core.Data;
using Blog.Application.Core.UseCases.Shared.Exceptions.Validation;
using Blog.Domain.Abstractions.Repositories;
using Blog.Domain.Abstractions.Services;

namespace Blog.Application.Core.UseCases.Tags.Commands.CreateTag;

internal class CreateTagCommandHandler(
    IApplicationDbContext dbContext,
    IFileService fileService,
    ISlugRepository slugRepository)
    : IRequestHandler<CreateTagCommand>
{
    public async Task Handle(CreateTagCommand command, CancellationToken cancellationToken)
    {
        // Image
        var imageName = await fileService.AddFileAsync(command.Image.FileName, command.Image.Content);
        await command.Image.Content.DisposeAsync();

        var tag = new Tag
        {
            Name = command.Name,
            Slug = SlugHelper.CreateSlug(command.Name),
            ImageFileName = imageName
        };

        // CheckSlug
        var slugAlreadyRegistered = await slugRepository.ExistsAsync(tag, cancellationToken);
        if (slugAlreadyRegistered)
        {
            throw new SlugAlreadyRegisteredException(tag.Slug);
        }

        dbContext.Tags.Add(tag);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}