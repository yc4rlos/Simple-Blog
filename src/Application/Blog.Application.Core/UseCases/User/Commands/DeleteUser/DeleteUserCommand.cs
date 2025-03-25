namespace Blog.Application.Core.UseCases.User.Commands.DeleteUser;

public record DeleteUserCommand(int Id): IRequest;