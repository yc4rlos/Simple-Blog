using Blog.Domain.Abstractions.Entities;
using Blog.Domain.Enums;

namespace Blog.Domain.Models;

public sealed class User: BaseEntity, ISlug
{
    public required string Name { get; set; }
    public required string Slug { get; set; }
    public string? ImageFileName { get; set; }
    public string Password { get; set; } = null!;
    public required string Login { get; set; }
    public required string Email { get; set; }
    public string? About { get; set; }
    public bool ResetPassword {get; set;}
    public required Role Role { get; set; }
    public List<Post> Posts { get; set; } = null!;
}