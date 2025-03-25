namespace Blog.Application.DTOs.Tag;

public class TagDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Slug { get; set; }
    public int PostCount { get; set; }
    public string? ImageFileName { get; set; }
}

public static class TagDtoExtensions
{
    public static TagDto ToTagDto(this Domain.Models.Tag tag)
    {
        return new TagDto
        {
            Id = tag.Id,
            Name = tag.Name,
            Slug = tag.Slug,
            ImageFileName = tag.ImageFileName,
            PostCount = tag.Posts?.Count ?? 0
        };
    }

    public static List<TagDto> ToTagDtos(this List<Domain.Models.Tag> tags)
    {
        return tags.Select(x => x.ToTagDto()).ToList();
    }
}