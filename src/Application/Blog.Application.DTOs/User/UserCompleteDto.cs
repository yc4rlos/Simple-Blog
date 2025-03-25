namespace Blog.Application.DTOs.User;

public class UserCompleteDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Slug { get; set; }
    public string? ImageFileName { get; set; }
    public required string Login { get; set; }
    public required string Email { get; set; }
    public string? About { get; set; }
    public required Role Role { get; set; }
}

public static class UserCompleteDtoExtensions
{
    public static UserCompleteDto ToUserCompleteDto(this Blog.Domain.Models.User user)
    {
        return new UserCompleteDto
        {
            Id = user.Id,
            Name = user.Name,
            Slug = user.Slug,
            ImageFileName = user.ImageFileName,
            Login = user.Login,
            Email = user.Email,
            About = user.About,
            Role = user.Role
        };
    }
}