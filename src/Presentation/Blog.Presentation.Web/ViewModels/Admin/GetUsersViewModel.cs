using Blog.Application.DTOs.User;

namespace Blog.Presentation.Web.ViewModels.Admin;

public class GetUsersViewModel
{
    public required IEnumerable<UserDto> Users { get; set; }
    public required int CurrentPage { get; set; }
    public required int LastPage { get; set; }
}