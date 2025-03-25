using Blog.Domain.Exceptions;

namespace Blog.Application.Core.UseCases.Shared.Exceptions.User;

public class AuthorNotFoundException(string slug): NotFoundException($"Author with Slug: '{slug}' was not found.");