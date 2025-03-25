namespace Blog.Application.Core.UseCases.User.Commands.Login;

public record LoginCommand(string Login, string Password): IRequest<LoginCommandResult>;

public record LoginCommandResult();