using Microsoft.AspNetCore.Http;

namespace Blog.Application.Core.Helpers;

public static class ValidateImageTypeHelper
{
    private static readonly string[] ValidImageTypes = { ".jpg", ".jpeg", ".png", ".gif" };
    
    public static bool Validate(IFormFile file)
    {
        var imageType = Path.GetExtension(file.FileName);

        if (ValidImageTypes.Contains(imageType))
        {
            return true;
        }

        return false;
    }
}