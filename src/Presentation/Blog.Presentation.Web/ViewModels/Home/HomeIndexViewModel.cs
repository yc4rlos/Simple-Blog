using Blog.Application.DTOs.Post;
using Blog.Application.DTOs.Tag;

namespace Blog.Presentation.Web.ViewModels.Home;

public class HomeIndexViewModel
{
    public required IEnumerable<PostDto> Posts { get; set; }
    public required int PostQuantity { get; set; }
    public required int LastPostPage { get; set; }
    public required IEnumerable<TagDto> Tags { get; set; }
    public required int TagTotalCount { get; set; }
}