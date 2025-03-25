using Blog.Domain.Models;

namespace Blog.Domain.Abstractions.Services;

public interface IEmailService
{
    Task SendNewUserMailAsync(User user, string password);
    Task SendTemporaryPasswordMailAsync(User user, string password);
}