using Blog.Application.Core.Data;
using Blog.Application.Core.UseCases.Shared.Exceptions.Post;

namespace Blog.Application.Core.UseCases.Posts.Commands.DeletePost;

internal class DeletePostCommandHandler(IApplicationDbContext dbContext)
    : IRequestHandler<DeletePostCommand>
{
    public async Task Handle(DeletePostCommand command, CancellationToken cancellationToken)
    {
        var post = await dbContext.Posts
            .FindAsync([command.Id], cancellationToken);

        if (post == null)
            throw new PostNotFoundException(command.Id);

        post.DeletedAt = DateTime.Now;

        dbContext.Posts.Update(post);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}