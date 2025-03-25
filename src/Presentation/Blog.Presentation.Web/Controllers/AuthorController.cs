using Blog.Application.Core.UseCases.Posts.Queries.GetPostsByUserSlug;
using Blog.Application.Core.UseCases.User.Queries.GetUserAuthorBySlug;
using Blog.Presentation.Web.ViewModels.Author;

namespace Blog.Presentation.Web.Controllers;

[Route("[controller]")]
public class AuthorController(ISender sender): Controller
{
    [HttpGet("{slug}")]
    public async Task<IActionResult> Index(string slug)
    {
        var authorResult =  await sender.Send(new GetUserAuthorBySlugQuery(slug));
        var postsResult = await sender.Send(new GetPostsByUserQuery(authorResult.UserAuthorDto.Id, 6));

        var viewModel = new AuthorViewModel
        {
            Author = authorResult.UserAuthorDto,
            Posts = postsResult.Posts
        };
        
        return View(viewModel);
    }
}