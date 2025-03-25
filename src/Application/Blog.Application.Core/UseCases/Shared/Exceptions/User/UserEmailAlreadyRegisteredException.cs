using Blog.Domain.Exceptions;

namespace Blog.Application.Core.UseCases.Shared.Exceptions.User;

public class UserEmailAlreadyRegisteredException(string email)
    : BadRequestException($"Email {email} already registered");