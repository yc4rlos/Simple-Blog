using Blog.Application.Core.Validators.Extensions;
using FluentValidation;

namespace Blog.Application.Core.UseCases.Posts.Commands.CreatePost;

public class CreatePostCommandValidator: AbstractValidator<CreatePostCommand>
{
    public CreatePostCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
        
        RuleFor(x => x.Content).NotEmpty();
        
        RuleFor(x => x.PostDate)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.Tags).NotEmpty();
        
        RuleFor(x => x.Summary).NotNull();
        
        RuleFor(x => x.Image).MustBeImage();
    }
}