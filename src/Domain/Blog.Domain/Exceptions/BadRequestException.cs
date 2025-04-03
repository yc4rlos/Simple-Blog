namespace Blog.Domain.Exceptions;

public class BadRequestException: Exception
{
    public List<string> Failures { get; } = new();
    public BadRequestException(string message): base(message)
    {
        
    }

    public BadRequestException(List<string> failures, string message): base(message)
    {
        Failures = failures;
    }
}