namespace Blog.Domain.ValueObjects;

public class BlobObject
{
    public required Stream Content { get; set; }
    public required string ContentType { get; set; }
}