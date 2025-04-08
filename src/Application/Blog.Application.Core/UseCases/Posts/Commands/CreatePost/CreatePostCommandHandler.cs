using Blog.Application.Core.Data;
using Blog.Application.Core.UseCases.Shared.Exceptions;
using Blog.Application.Core.UseCases.Shared.Exceptions.Validation;
using Blog.Application.Core.Validators.CustomFluentValidations;
using Blog.Domain.Abstractions.Repositories;
using Blog.Domain.Abstractions.Services;

namespace Blog.Application.Core.UseCases.Posts.Commands.CreatePost;

internal class CreatePostCommandHandler(
    IApplicationDbContext dbContext,
    IFileService fileService,
    ISlugRepository slugRepository)
    : IRequestHandler<CreatePostCommand>
{
    public async Task Handle(CreatePostCommand command, CancellationToken cancellationToken)
    {
        var postEntity = command.ToEntity();

        // Image
        postEntity.ImageFileName = await fileService.AddFileAsync(command.Image.FileName, command.Image.Content);
        await command.Image.Content.DisposeAsync();

        // Check Slug
        var slugAlreadyRegistered = await slugRepository.ExistsAsync(postEntity, cancellationToken);
        if (slugAlreadyRegistered)
            throw new SlugAlreadyRegisteredException(postEntity.Slug);

        // Read Time
        postEntity.ReadTimeMinutes = ReadTimeHelper.GetReadTimeMinutes(postEntity.Content);

        dbContext.Posts.Add(postEntity);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}