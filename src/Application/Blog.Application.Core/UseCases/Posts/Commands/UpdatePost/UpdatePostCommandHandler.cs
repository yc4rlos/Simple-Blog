using Blog.Application.Core.Data;
using Blog.Application.Core.UseCases.Shared.Exceptions.Post;
using Blog.Application.Core.UseCases.Shared.Exceptions.Validation;
using Blog.Domain.Abstractions.Repositories;
using Blog.Domain.Abstractions.Services;

namespace Blog.Application.Core.UseCases.Posts.Commands.UpdatePost;

internal class UpdatePostCommandHandler(
    IApplicationDbContext dbContext,
    IFileService fileService,
    ISlugRepository slugRepository)
    : IRequestHandler<UpdatePostCommand>
{
    public async Task Handle(UpdatePostCommand command, CancellationToken cancellationToken)
    {
        var postEntity = await dbContext.Posts.FindAsync([command.Id], cancellationToken);

        if (postEntity == null)
            throw new PostNotFoundException(command.Id);

        // Image
        if (command.Image != null)
        {
            await fileService.DeleteFileAsync(postEntity.ImageFileName, cancellationToken);
            await using var stream = command.Image.OpenReadStream();
            postEntity.ImageFileName = await fileService.AddFileAsync(command.Image.FileName, stream);
        }

        var titleChanged = command.Title != postEntity.Title;

        command.ToEntity(postEntity);

        // Read Time
        postEntity.ReadTimeMinutes = ReadTimeHelper.GetReadTimeMinutes(postEntity.Content);

        // Check Slug
        if (titleChanged)
        {
            var slugAlreadyRegistered = await slugRepository.ExistsAsync(postEntity, cancellationToken);
            if (slugAlreadyRegistered)
                throw new SlugAlreadyRegisteredException(postEntity.Slug);
        }

        await UpdateTags(postEntity, cancellationToken);

        dbContext.Posts.Update(postEntity);

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task UpdateTags(Post post, CancellationToken cancellationToken)
    {
        var currentTags = await dbContext.PostTags
            .AsNoTracking()
            .Where(x => x.PostId == post.Id)
            .ToListAsync(cancellationToken);

        // Old Tags
        foreach (var tag in currentTags)
        {
            var keepRegistered = post.Tags.Exists(x => x.TagId == tag.TagId);
            if (!keepRegistered)
            {
                dbContext.PostTags.Remove(tag);
            }
        }

        // New Tags
        foreach (var tag in post.Tags)
        {
            var shouldBeRegistered = !currentTags.Exists(x => x.TagId == tag.TagId);
            if (shouldBeRegistered)
            {
                await dbContext.PostTags.AddAsync(tag, cancellationToken);
            }
        }

        post.Tags.Clear();
    }
}