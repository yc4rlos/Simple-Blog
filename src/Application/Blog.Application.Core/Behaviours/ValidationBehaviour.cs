using Blog.Domain.Exceptions;
using FluentValidation;

namespace Blog.Application.Core.Behaviours;

public class ValidationBehaviour<TRequest, TResponse> (IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest
{
    public async Task<TResponse> Handle(
        TRequest request, 
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);

        var validationResults = await Task.WhenAll(
            validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        var failures = validationResults
            .Where(x => !x.IsValid)
            .SelectMany(x => x.Errors)
            .ToList();

        if (failures.Count > 0)
        {
            var errors = failures.Select(x => x.ErrorMessage).ToList();
            throw new BadRequestException(errors, "One or more validation failures have occurred.");
        }
        
        return await next();
    }
}