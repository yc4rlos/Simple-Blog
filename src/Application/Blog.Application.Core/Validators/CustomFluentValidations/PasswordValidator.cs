using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Validators;

namespace Blog.Application.Core.Validators.CustomFluentValidations;

public class PasswordValidator<T>: PropertyValidator<T,string>
{
    public override string Name => "PasswordValidator";

    public override bool IsValid(ValidationContext<T> context, string value)
    {
        const int minLength = 8;
        var hasLowercase = new Regex("[a-z]+");
        var hasUppercase = new Regex("[A-Z]+");
        var hasNumber = new Regex("(\\d)+");
        var hasSpecialCharacters = new Regex("(\\W)+");

        return (
            value.Length >= minLength &&
            hasLowercase.IsMatch(value) && 
            hasUppercase.IsMatch(value) && 
            hasNumber.IsMatch(value) && 
            hasSpecialCharacters.IsMatch(value)
        );
    }

    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        return "{PropertyName} must contains at least: 1 Uppercase letter, one lowercase letter, one number and one special character.";
    }
}