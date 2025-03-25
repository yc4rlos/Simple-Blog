namespace Blog.Application.DTOs.Post;

public class PostDto
{
    public required int Id { get; set; }
    public required string Title { get; set; }
    public required string Slug { get; set; }
    public required string? Summary { get; set; }
    public List<string> Tags { get; set; } = new();
    public string ImageFileName { get; set; } = null!;
    public required bool NewPost { get; set; } = true;
    public required DateTime PostDate { get; set; }
    public int ReadTimeMinutes { get; set; }
    public required string AuthorName { get; set; }
    public string? AuthorImageFileName { get; set; }
}

public static class PostDtoExtensions
{
    public static PostDto ToDto(this Domain.Models.Post post)
    {
        var result = new PostDto
        {
            Id = post.Id,
            Title = post.Title,
            Slug = post.Slug,
            Summary = post.Summary,
            ImageFileName = post.ImageFileName,
            NewPost = post.PostDate > DateTime.Now.AddDays(-3),
            PostDate = post.PostDate,
            ReadTimeMinutes = post.ReadTimeMinutes,
            AuthorName = post.CreatedBy.Name,
            AuthorImageFileName = post.CreatedBy.ImageFileName
        };

        if (post.Tags?.Count > 0)
            result.Tags = post.Tags.Select(x => x.Tag.Name).ToList();

        return result;
    }

    public static IEnumerable<PostDto> ToDto(this List<Domain.Models.Post> posts)
    {
        return posts.Select(x => x.ToDto());
    }
}