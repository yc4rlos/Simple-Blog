using Blog.Domain.Enums;
using Blog.Domain.Models;

namespace Blog.Infrastructure.Data.InitialData;

public static class UserInitialData
{
    public static IEnumerable<User> Data => new List<User>
    {
        new User
        {
            Name = "Author",
            Slug = "author",
            Login = "author",
            Email = "author@example.com",
            Role = Role.Author,
            Password = "$2a$11$/Sy9bhuVPG3RoJrIAIjX5uZmZcj6jys4E/p2dgKSb5OQoJoh7VQoa"
        }
    };
}