using Blog.Domain.Abstractions.Entities;

namespace Blog.Domain.Models;

public sealed class Tag : BaseEntity, ISlug
{
    public required string Name { get; set; }
    public required string Slug { get; set; }
    public string? ImageFileName { get; set; }
    public List<PostTag> Posts { get; set; } = null!;
}