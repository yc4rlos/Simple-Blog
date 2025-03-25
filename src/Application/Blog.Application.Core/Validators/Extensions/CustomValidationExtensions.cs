using Blog.Application.Core.Validators.CustomFluentValidations;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Blog.Application.Core.Validators.Extensions;

public static class CustomValidationExtensions
{
    public static IRuleBuilderOptions<T, IFormFile?> MustBeImage<T>(this IRuleBuilder<T, IFormFile?> ruleBuilder) {
        return ruleBuilder.SetValidator(new ImageValidator<T>());
    }

    public static IRuleBuilderOptions<T, string> Password<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.SetValidator(new PasswordValidator<T>());
    }
}