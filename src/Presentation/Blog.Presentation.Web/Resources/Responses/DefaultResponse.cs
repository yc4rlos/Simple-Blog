namespace Blog.Presentation.Web.Resources.Responses;

public class DefaultResponse : DefaultResponse<string?>;

public class DefaultResponse<T>
{
    public required string Message { get; set; } = string.Empty;
    public T Data { get; set; } = default!;
    public List<string> Errors { get; set; } = new();
}