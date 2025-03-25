namespace Blog.Domain.Abstractions.Services;

public interface IAuthService
{
    int? GetCurrentUserId();
}