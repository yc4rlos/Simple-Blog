using Blog.Application.DTOs.Tag;

namespace Blog.Application.DTOs.Post;

public class PostCompleteDto
{
    public required int Id { get; set; }
    public required string Title { get; set; }
    public required string Content { get; set; }
    public required string Summary { get; set; }
    public required DateTime PostDate { get; set; }
    public string ImageFileName { get; set; } = null!;
    public IEnumerable<TagDto> Tags { get; set; } = null!;
    public required string AuthorName { get; set; }
    public string? AuthorImageFileName { get; set; }
}

public static class PostCompleteDtoExtensions
{
    public static PostCompleteDto ToPostCompleteDto(this Domain.Models.Post post)
    {
        return new PostCompleteDto
        {
            Id = post.Id,
            Title = post.Title,
            Summary = post.Summary,
            Content = post.Content,
            ImageFileName = post.ImageFileName,
            PostDate = post.PostDate,
            Tags = post.Tags.Select(x => x.Tag.ToTagDto()),
            AuthorName = post.CreatedBy.Name,
            AuthorImageFileName = post.CreatedBy.ImageFileName,
        };
    }
}