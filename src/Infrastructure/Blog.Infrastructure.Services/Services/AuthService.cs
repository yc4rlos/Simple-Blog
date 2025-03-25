using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Blog.Infrastructure.Services.Services;

public class AuthService(IHttpContextAccessor httpContextAccessor) : IAuthService
{
    public int? GetCurrentUserId()
    {
        var idString = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (idString is null) return null;

        return int.Parse(idString);
    }
}