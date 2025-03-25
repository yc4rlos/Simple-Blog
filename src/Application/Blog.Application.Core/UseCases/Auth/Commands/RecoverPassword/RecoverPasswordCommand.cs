namespace Blog.Application.Core.UseCases.Auth.Commands.RecoverPassword;

public record RecoverPasswordCommand(string Email): IRequest;