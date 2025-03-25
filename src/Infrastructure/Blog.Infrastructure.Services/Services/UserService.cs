namespace Blog.Infrastructure.Services.Services;

public class UserService(ApplicationDbContext dbContext): IUserService
{
    public Task<bool> EmailAlreadyExists(string email, CancellationToken cancellationToken)
    {
        return dbContext.Users
            .AnyAsync(u => u.Email == email, cancellationToken);
    }
}