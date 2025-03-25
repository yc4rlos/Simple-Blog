using FluentValidation;

namespace Blog.Application.Core.UseCases.User.Commands.Login;

public class LoginCommandValidator: AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        
    }
}