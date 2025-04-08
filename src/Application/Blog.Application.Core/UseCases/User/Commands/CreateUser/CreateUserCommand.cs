using Blog.Domain.Enums;
using Blog.Application.Core.Common;

namespace Blog.Application.Core.UseCases.User.Commands.CreateUser;

public record CreateUserCommand(
    string Name,
    string Email,
    string? About,
    string Login,
    Role Role,
    FileUpload? Image) : IRequest;

public static class CreateUserCommandExtensions
{
    public static Domain.Models.User ToEntity(this CreateUserCommand command)
    {
        return new Domain.Models.User
        {
            Name = command.Name,
            Slug = SlugHelper.CreateSlug(command.Name),
            Email = command.Email,
            Login = command.Login,
            About = command.About?.Trim().Replace(@"&nbsp;", " "),
            Role = command.Role
        };
    }
}