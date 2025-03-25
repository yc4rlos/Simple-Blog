using Blog.Application.Core.UseCases.Posts.Queries.GetPostsByTagQuery;
using Blog.Application.Core.UseCases.Tags.Commands.CreateTag;
using Blog.Application.Core.UseCases.Tags.Commands.DeleteTag;
using Blog.Application.Core.UseCases.Tags.Commands.UpdateTag;
using Blog.Application.Core.UseCases.Tags.Queries.GetTagBySlug;
using Blog.Application.Core.UseCases.Tags.Queries.GetTags;
using Blog.Presentation.Web.ViewModels.Tag;

namespace Blog.Presentation.Web.Controllers;

[Route("[controller]")]
public class TagController(ISender sender) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var tagsResult = await sender.Send(new GetTagsQuery());
        return View(tagsResult.Tags);
    }

    [HttpGet("{slug}")]
    public async Task<IActionResult> Get(string slug)
    {
        var tagResult = await sender.Send(new GetTagBySlugQuery(slug));
        var postsResult = await sender.Send(new GetPostsByTagQuery(slug));

        var viewModel = new GetTagViewModel
        {
            Tag = tagResult.Tag,
            Posts = postsResult.Posts
        };

        return View(viewModel);
    }

    
}