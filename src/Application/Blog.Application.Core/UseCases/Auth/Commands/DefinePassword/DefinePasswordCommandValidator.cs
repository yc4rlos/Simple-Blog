using Blog.Application.Core.Validators.Extensions;
using FluentValidation;

namespace Blog.Application.Core.UseCases.Auth.Commands.DefinePassword;

public class DefinePasswordCommandValidator: AbstractValidator<DefinePasswordCommand>
{
    public DefinePasswordCommandValidator()
    {
        RuleFor(v => v.Password)
            .NotNull()
            .Password();
    }
}