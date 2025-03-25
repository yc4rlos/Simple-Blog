using Blog.Domain.Abstractions.Entities;

namespace Blog.Domain.Models;

public sealed class Post: BaseEntity, ISlug
{
    public required string Title { get; set; }
    public required string Slug { get; set; }
    public required string Content { get; set; }
    public required string Summary { get; set; }
    public int ReadTimeMinutes { get; set; }
    public string ImageFileName { get; set; } = null!;
    public required DateTime PostDate { get; set; }
    public List<PostTag> Tags { get; set; } = null!;
    public User CreatedBy { get; set; } = null!;
}