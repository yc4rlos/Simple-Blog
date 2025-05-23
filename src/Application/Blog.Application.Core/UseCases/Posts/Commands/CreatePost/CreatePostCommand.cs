using Blog.Application.Core.Common;

namespace Blog.Application.Core.UseCases.Posts.Commands.CreatePost;

public record CreatePostCommand(
    string Title,
    string Content,
    string Summary,
    DateTime PostDate,
    List<int> Tags,
    FileUpload Image
) : IRequest;


public static class CreatePostCommandExtension
{
    public static Post ToEntity(this CreatePostCommand command)
    {
        return new Post
        {
            Title = command.Title.Trim(),
            Slug = SlugHelper.CreateSlug(command.Title),
            Content = command.Content.Trim().Replace(@"&nbsp;", " "),
            Summary = command.Summary.Trim(),
            PostDate = command.PostDate,
            Tags = command.Tags.Select(x => new PostTag { TagId = x }).ToList()
        };
    }
}