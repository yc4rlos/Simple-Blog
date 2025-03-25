using System.Security.Claims;
using Blog.Application.Core.Data;

namespace Blog.Application.Core.UseCases.Auth.Commands.Login;

internal class LoginCommandHandler (IApplicationDbContext dbContext)
    : IRequestHandler<LoginCommand, LoginCommandResult>
{
    public async Task<LoginCommandResult> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users
            .FirstOrDefaultAsync(x => x.Login.ToLower() == request.Login.Trim().ToLower(), cancellationToken);
        
        if(user == null) return new LoginCommandResult(false);

        var success =
            BCrypt.Net.BCrypt.Verify(request.Password, user.Password);

        if (!success) return new LoginCommandResult(false); 
        
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
            new Claim("ImageFileName", user?.ImageFileName ?? string.Empty)
        };
        
        return new LoginCommandResult(true, claims, user is { ResetPassword: true });
    }
}