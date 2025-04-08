using Blog.Application.Core.Common;
using Blog.Application.Core.UseCases.Posts.Commands.CreatePost;
using Blog.Application.Core.UseCases.Posts.Commands.UpdatePost;

namespace Blog.Presentation.Web.Requests.Post;

public static class CreateOrUpdatePostRequestExtensions
{
    public static CreatePostCommand ToCreateCommand(this CreateOrUpdatePostRequest request)
    {
        return new CreatePostCommand(
            Title: request.Title,
            Content: request.Content,
            Summary: request.Summary,
            PostDate: request.PostDate,
            Tags: request.Tags,
            Image: new FileUpload
            {
                FileName = request.Image?.FileName  ?? string.Empty,
                ContentType = request.Image?.ContentType ?? string.Empty,
                Content = request.Image?.OpenReadStream() ?? null!
            }
        );
    }

    public static UpdatePostCommand ToUpdateCommand(this CreateOrUpdatePostRequest request, int id)
    {
        return new UpdatePostCommand(
            Id: id,
            Title: request.Title,
            Content: request.Content,
            Summary: request.Summary,
            PostDate: request.PostDate,
            Tags: request.Tags,
            Image: request.Image != null ? new FileUpload
            {
                FileName = request.Image.FileName,
                ContentType = request.Image.ContentType,
                Content = request.Image.OpenReadStream()
            } : null
        );
    }
}
