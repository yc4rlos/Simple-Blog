using Blog.Application.Core.Common;
using Blog.Application.Core.UseCases.Tags.Commands.CreateTag;
using Blog.Application.Core.UseCases.Tags.Commands.UpdateTag;

namespace Blog.Presentation.Web.Requests.Tag;

public static class CreateOrUpdateTagRequestExtensions
{
    public static CreateTagCommand ToCreateCommand(this CreateOrUpdateTagRequest request)
    {
        return new CreateTagCommand(
            Name: request.Name,
            Image: new FileUpload
            {
                FileName = request.Image.FileName,
                ContentType = request.Image.ContentType,
                Content = request.Image.OpenReadStream()
            }
        );
    }

    public static UpdateTagCommand ToUpdateCommand(this CreateOrUpdateTagRequest request, int id)
    {
        return new UpdateTagCommand(
            Id: id,
            Name: request.Name,
            Image: request.Image != null ? new FileUpload
            {
                FileName = request.Image.FileName,
                ContentType = request.Image.ContentType,
                Content = request.Image.OpenReadStream()
            } : null
        );
    }
}
