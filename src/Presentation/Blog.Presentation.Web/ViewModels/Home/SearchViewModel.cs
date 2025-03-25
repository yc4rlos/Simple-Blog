using Blog.Application.DTOs.Post;
using Blog.Application.DTOs.Tag;

namespace Blog.Presentation.Web.ViewModels.Home;

public class SearchViewModel
{
    public required IEnumerable<PostDto> Posts { get; set; }
    public required IEnumerable<TagDto> Tags { get; set; }
}