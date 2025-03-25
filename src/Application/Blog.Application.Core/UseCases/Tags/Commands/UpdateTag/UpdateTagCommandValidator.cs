using Blog.Application.Core.Validators.Extensions;
using FluentValidation;

namespace Blog.Application.Core.UseCases.Tags.Commands.UpdateTag;

public class UpdateTagCommandValidator: AbstractValidator<UpdateTagCommand>
{
    public UpdateTagCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Image).MustBeImage();
    }
}