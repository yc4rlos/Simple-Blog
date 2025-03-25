using Blog.Domain.Exceptions;

namespace Blog.Application.Core.UseCases.Shared.Exceptions.User;

public class UserNotFoundException: NotFoundException
{
    public UserNotFoundException(int id): base($"User with id {id} not found") {}

    public UserNotFoundException(string email): base($"User with email '{email}' not found") {}
};