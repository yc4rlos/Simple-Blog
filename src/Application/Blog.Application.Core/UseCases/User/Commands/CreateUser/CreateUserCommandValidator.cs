using Blog.Application.Core.Validators.Extensions;
using FluentValidation;

namespace Blog.Application.Core.UseCases.User.Commands.CreateUser;

public class CreateUserCommandValidator: AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(v => v.Name).NotEmpty().MinimumLength(5);
        RuleFor(x => x.Role).NotNull();
        RuleFor(v => v.Login).NotEmpty().MinimumLength(3);
        RuleFor(v => v.Email).NotEmpty().EmailAddress();
        RuleFor(v => v.Image).MustBeImage();
    }
}