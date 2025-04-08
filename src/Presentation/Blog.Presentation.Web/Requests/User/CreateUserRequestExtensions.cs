using Blog.Application.Core.UseCases.User.Commands.CreateUser;
using Blog.Application.Core.Common;
using Blog.Application.Core.UseCases.User.Commands.UpdateUser;

namespace Blog.Presentation.Web.Requests.User;

public static class CreateOrUpdateUserRequestExtensions
{
    public static CreateUserCommand ToCreateCommand(this CreateOrUpdateUserRequest request)
    {
        return new CreateUserCommand(
            Name: request.Name,
            Email: request.Email,
            About: request.About,
            Login: request.Login,
            Role: request.Role,
            Image: request.Image != null ?
            new FileUpload
            {
                FileName = request.Image.FileName,
                ContentType = request.Image.ContentType,
                Content = request.Image.OpenReadStream()
            } : null
        );
    }

    public static UpdateUserCommand ToUpdateCommand(this CreateOrUpdateUserRequest request, int id)
    {
        return new UpdateUserCommand(
            Id: id,
            Name: request.Name,
            Email: request.Email,
            About: request.About,
            Login: request.Login,
            Role: request.Role,
            Image: request.Image != null ?
            new FileUpload
            {
                FileName = request.Image.FileName,
                ContentType = request.Image.ContentType,
                Content = request.Image.OpenReadStream()
            } : null
        );
    }
}
