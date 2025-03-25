using Blog.Application.Core.Validators.Extensions;
using FluentValidation;

namespace Blog.Application.Core.UseCases.Tags.Commands.CreateTag;

public class CreateTagCommandValidator: AbstractValidator<CreateTagCommand>
{
    public CreateTagCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();

        RuleFor(x => x.Image)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.Image).MustBeImage();
    }
}