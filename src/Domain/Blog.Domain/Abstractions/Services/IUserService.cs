namespace Blog.Domain.Abstractions.Services;

public interface IUserService
{
    Task<bool> EmailAlreadyExists(string email, CancellationToken cancellationToken);
}