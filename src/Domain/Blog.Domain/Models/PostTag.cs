namespace Blog.Domain.Models;

public sealed class PostTag: BaseEntity
{
    public int TagId { get; set; }
    public int PostId { get; set; }
    public Post Post { get; set; } = null!;
    public Tag Tag { get; set; } = null!;
}