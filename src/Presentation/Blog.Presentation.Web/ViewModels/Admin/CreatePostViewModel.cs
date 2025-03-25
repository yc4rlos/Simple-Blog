using Blog.Application.DTOs.Post;
using Blog.Application.DTOs.Tag;

namespace Blog.Presentation.Web.ViewModels.Admin;

public class CreatePostViewModel
{
    public string SlugsJsonString { get; set; } = "[]";
    public PostCompleteDto? Post { get; set; }
    public required IEnumerable<TagDto> Tags { get; set; }
}