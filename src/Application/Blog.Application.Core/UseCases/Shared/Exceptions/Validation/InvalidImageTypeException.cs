using Blog.Domain.Exceptions;

namespace Blog.Application.Core.UseCases.Shared.Exceptions.Validation;

public class InvalidImageTypeException(): BadRequestException("Invalid image type");