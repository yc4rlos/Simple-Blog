namespace Blog.Application.DTOs.User;

public class UserAuthorDto
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string? ImageFileName { get; set; }
    public required string? About { get; set; }
}

public static class UserAuthorDtoExtensions
{
    public static UserAuthorDto ToUserAuthorDto(this Domain.Models.User user)
    {
        return new UserAuthorDto
        {
            Id = user.Id,
            Name = user.Name,
            ImageFileName = user.ImageFileName,
            About = user.About
        };
    }
}