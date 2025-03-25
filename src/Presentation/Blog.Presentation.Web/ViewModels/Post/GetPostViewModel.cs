using Blog.Application.DTOs.Post;

namespace Blog.Presentation.Web.ViewModels.Post;

public class GetPostViewModel
{
    public required PostCompleteDto Post { get; set; }
    public required IEnumerable<PostDto> RelatedPosts { get; set; }
}