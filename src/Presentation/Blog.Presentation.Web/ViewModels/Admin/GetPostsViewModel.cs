using Blog.Application.DTOs.Post;

namespace Blog.Presentation.Web.ViewModels.Admin;

public class GetPostsViewModel
{
    public required IEnumerable<PostDto> Posts { get; set; }
    public required int PostQuantity { get; set; }
    public required int CurrentPage { get; set; }
    public required int LastPage { get; set; }
}