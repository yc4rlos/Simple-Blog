using Blog.Application.Core.Common;
using Blog.Domain.Enums;

namespace Blog.Application.Core.UseCases.User.Commands.UpdateUser;

public record UpdateUserCommand(
    int Id,
    string Name,
    string Email,
    string? About,
    string Login,
    FileUpload? Image,
    Role Role) : IRequest;

public static class UpdateUserCommandExtensions
{
    public static Domain.Models.User ToEntity(this UpdateUserCommand command, Domain.Models.User user)
    {
        user.Name = command.Name;
        user.Slug = SlugHelper.CreateSlug(command.Name);
        user.Email = command.Email;
        user.Login = command.Login;
        user.About = command.About?.Trim().Replace(@"&nbsp;", " ");
        user.Role = command.Role;

        return user;
    }
}