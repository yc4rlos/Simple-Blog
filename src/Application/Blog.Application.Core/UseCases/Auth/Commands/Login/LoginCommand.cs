using System.Security.Claims;

namespace Blog.Application.Core.UseCases.Auth.Commands.Login;

public record LoginCommand(string Login, string Password, bool RememberMe) : IRequest<LoginCommandResult>;

public record LoginCommandResult(bool Success, List<Claim>? Claims = null, bool ResetPassword = false);