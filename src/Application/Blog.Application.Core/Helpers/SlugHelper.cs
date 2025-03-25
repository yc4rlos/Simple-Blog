namespace Blog.Application.Core.Helpers;

public static class SlugHelper
{
    public static string CreateSlug(string text)
    {
        return text.ToLower().Trim().Replace(" ", "-");
    }
}