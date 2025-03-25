using Blog.Application.Core.UseCases.Posts.Queries.GetPosts;
using Blog.Application.Core.UseCases.Tags.Queries.GetTags;
using Blog.Presentation.Web.ViewModels.Home;

namespace Blog.Presentation.Web.Controllers;

public class HomeController(ISender sender) : Controller
{
    public async Task<IActionResult> Index()
    {
        const int postQuantity = 6;

        var postsResult = await sender.Send(new GetPostsQuery(postQuantity, 1, true));
        var tagsResult = await sender.Send(new GetTagsQuery(4, 1, false, CountOnlyPostedPosts: true));

        var result = new HomeIndexViewModel
        {
            Posts = postsResult.Posts,
            PostQuantity = postQuantity,
            LastPostPage = postsResult.LastPage,
            Tags = tagsResult.Tags,
            TagTotalCount = tagsResult.TotalCount,
        };
        return View(result);
    }

    public async Task<IActionResult> Search(string value)
    {
        var postsResult = await sender.Send(new GetPostsQuery(OnlyPosted: true, SearchTerm: value));
        var tagsResult = await sender.Send(new GetTagsQuery(IncludeEmpty: false, SearchTerm: value));

        var viewModel = new SearchViewModel
        {
            Posts = postsResult.Posts,
            Tags = tagsResult.Tags,
        };
        return View(viewModel);
    }
}