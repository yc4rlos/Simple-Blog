using Blog.Domain.Models;

namespace Blog.Infrastructure.Data.InitialData;

public static class TagInitialData
{
    public static IEnumerable<Tag> Data => new List<Tag>
    {
        new Tag
        {
            Name = "Test",
            Slug = "test",
            ImageFileName = "test.jpg",
            CreatedAt = DateTime.MinValue,
        }
    };
}