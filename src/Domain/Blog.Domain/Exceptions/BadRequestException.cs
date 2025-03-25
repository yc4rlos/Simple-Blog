using FluentValidation.Results;

namespace Blog.Domain.Exceptions;

public class BadRequestException: Exception
{
    public List<ValidationFailure> Failures { get; } = new();
    public BadRequestException(string message): base(message)
    {
        
    }

    public BadRequestException(List<ValidationFailure> failures, string message): base(message)
    {
        Failures = failures;
    }
}