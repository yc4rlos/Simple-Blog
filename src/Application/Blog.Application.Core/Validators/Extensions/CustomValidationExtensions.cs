using Blog.Application.Core.Common;
using Blog.Application.Core.Validators.CustomFluentValidations;
using FluentValidation;

namespace Blog.Application.Core.Validators.Extensions;

public static class CustomValidationExtensions
{
    public static IRuleBuilderOptions<T, FileUpload?> MustBeImage<T>(this IRuleBuilder<T, FileUpload?> ruleBuilder)
    {
        return ruleBuilder.SetValidator(new ImageValidator<T>());
    }

    public static IRuleBuilderOptions<T, string> Password<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.SetValidator(new PasswordValidator<T>());
    }
}