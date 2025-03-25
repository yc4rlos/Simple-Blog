using System.Text.Json;
using Blog.Application.Core.UseCases.Posts.Commands.CreatePost;
using Blog.Application.Core.UseCases.Posts.Commands.DeletePost;
using Blog.Application.Core.UseCases.Posts.Commands.UpdatePost;
using Blog.Application.Core.UseCases.Posts.Queries.GetPost;
using Blog.Application.Core.UseCases.Posts.Queries.GetPosts;
using Blog.Application.Core.UseCases.Posts.Queries.GetPostsByTagQuery;
using Blog.Application.Core.UseCases.Tags.Queries.GetTags;
using Blog.Presentation.Web.ViewModels.Admin;
using Blog.Presentation.Web.ViewModels.Post;

namespace Blog.Presentation.Web.Controllers;

[Route("[controller]")]
public class PostController(ISender sender) : Controller
{
    [HttpGet("posts-partial")]
    public async Task<IActionResult> GetPostsPartial(GetPostsQuery query)
    {
        var postsResult = await sender.Send(query);
        return PartialView("_PostListPartial", postsResult.Posts);
    }

    [HttpGet("{slug}")]
    public async Task<IActionResult> Get(GetPostQuery query)
    {
        var postResult = await sender.Send(query);
        var tag = postResult.Post.Tags.First();

        var relatedPostsResult = await sender.Send(new GetPostsByTagQuery(tag.Slug, 4));

        var relatedPosts = relatedPostsResult.Posts.Where(x => x.Id != postResult.Post.Id);

        var viewModel = new GetPostViewModel
        {
            Post = postResult.Post,
            RelatedPosts = relatedPosts
        };
        return View(viewModel);
    }
}