using Blog.Application.DTOs.Tag;

namespace Blog.Presentation.Web.ViewModels.Admin;

public class GetTagsViewModel
{
    public string SearchTerm { get; set; } = "";
    public required IEnumerable<TagDto> Tags { get; set; }
    public required int CurrentPage { get; set; }
    public required int LastPage { get; set; }
}