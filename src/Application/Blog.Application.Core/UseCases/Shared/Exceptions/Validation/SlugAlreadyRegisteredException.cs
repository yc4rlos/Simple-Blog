using Blog.Domain.Exceptions;

namespace Blog.Application.Core.UseCases.Shared.Exceptions.Validation;

public class SlugAlreadyRegisteredException(string slug)
    : BadRequestException($"The slug '{slug}' is already registered");