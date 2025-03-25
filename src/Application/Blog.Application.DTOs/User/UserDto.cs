namespace Blog.Application.DTOs.User;

public class UserDto
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public string? Slug { get; set; }
    public required string Email { get; set; }
    public required Role Role { get; set; }
}

public static class UserDtoExtensions
{
    public static IEnumerable<UserDto> ToUserDtos(this List<Domain.Models.User> users)
    {
        return users.Select(x => new UserDto
        {
            Id = x.Id,
            Name = x.Name,
            Slug = x.Slug,
            Email = x.Email,
            Role = x.Role
        });
    }
}