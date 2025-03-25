namespace Blog.Application.Core.UseCases.Auth.Commands.DefinePassword;

public record DefinePasswordCommand(string Password) : IRequest;