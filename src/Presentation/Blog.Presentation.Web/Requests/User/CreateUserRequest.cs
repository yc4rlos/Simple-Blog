namespace Blog.Presentation.Web.Requests;

public record CreateOrUpdateUserRequest(
    string Name,
    string Email,
    string? About,
    string Login,
    Role Role,
    IFormFile? Image
);