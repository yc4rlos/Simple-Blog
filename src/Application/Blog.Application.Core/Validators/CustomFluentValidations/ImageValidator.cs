using Blog.Application.Core.Common;
using FluentValidation;
using FluentValidation.Validators;

namespace Blog.Application.Core.Validators.CustomFluentValidations;

public class ImageValidator<T> : PropertyValidator<T, FileUpload?>
{
    public override string Name => "ImageValidator";
    private readonly string[] _fileTypes = [".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp"];

    public override bool IsValid(ValidationContext<T> context, FileUpload? value)
    {
        if (value == null)
            return true;

        var fileExtension = Path.GetExtension(value.FileName);
        return _fileTypes.Contains(fileExtension);
    }

    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        var fileTypesAccepted = string.Join(", ", _fileTypes);
        return "{PropertyName} must be one of the following: " + fileTypesAccepted;
    }
}