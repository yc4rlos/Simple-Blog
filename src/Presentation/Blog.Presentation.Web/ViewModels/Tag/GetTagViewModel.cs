using Blog.Application.DTOs.Post;
using Blog.Application.DTOs.Tag;

namespace Blog.Presentation.Web.ViewModels.Tag;

public class GetTagViewModel
{
    public required TagDto Tag { get; set; }
    public required IEnumerable<PostDto> Posts { get; set; }
}