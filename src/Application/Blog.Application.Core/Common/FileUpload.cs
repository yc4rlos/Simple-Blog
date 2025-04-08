namespace Blog.Application.Core.Common;

public class FileUpload
{
    public required string FileName { get; init; }
    public required string ContentType { get; init; }
    public required Stream Content { get; init; }
}
