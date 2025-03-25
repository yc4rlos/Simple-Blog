using Blog.Application.Core.Data;
using Blog.Application.Core.UseCases.Shared.Exceptions.Tag;

namespace Blog.Application.Core.UseCases.Tags.Commands.DeleteTag;

internal class DeleteTagCommandHandler(IApplicationDbContext dbContext)
    : IRequestHandler<DeleteTagCommand>
{
    public async Task Handle(DeleteTagCommand command, CancellationToken cancellationToken)
    {
        var tag = await dbContext.Tags.FindAsync([command.Id], cancellationToken: cancellationToken);

        if (tag == null)
            throw new TagNotFoundException(command.Id);

        var tagInUse = await dbContext.PostTags
            .AnyAsync(x => x.TagId == command.Id, cancellationToken);

        if (tagInUse)
            throw new CantDeleteTagInUseException(command.Id);

        tag.DeletedAt = DateTime.Now;
        dbContext.Tags.Update(tag);
        
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}