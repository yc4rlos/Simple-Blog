using System.Text.Json;
using Blog.Application.Core.UseCases.Posts.Commands.CreatePost;
using Blog.Application.Core.UseCases.Posts.Commands.DeletePost;
using Blog.Application.Core.UseCases.Posts.Commands.UpdatePost;
using Blog.Application.Core.UseCases.Posts.Queries.GetPost;
using Blog.Application.Core.UseCases.Posts.Queries.GetPosts;
using Blog.Application.Core.UseCases.Tags.Commands.CreateTag;
using Blog.Application.Core.UseCases.Tags.Commands.DeleteTag;
using Blog.Application.Core.UseCases.Tags.Commands.UpdateTag;
using Blog.Application.Core.UseCases.Tags.Queries.GetTagBySlug;
using Blog.Application.Core.UseCases.Tags.Queries.GetTags;
using Blog.Application.Core.UseCases.User.Commands.CreateUser;
using Blog.Application.Core.UseCases.User.Commands.DeleteUser;
using Blog.Application.Core.UseCases.User.Commands.UpdateUser;
using Blog.Application.Core.UseCases.User.Queries.GetUserById;
using Blog.Application.Core.UseCases.User.Queries.GetUsers;
using Blog.Domain.Enums;
using Blog.Presentation.Web.Resources.Attributes;
using Blog.Presentation.Web.ViewModels.Admin;
using Microsoft.AspNetCore.Authorization;

namespace Blog.Presentation.Web.Controllers;

[Authorize]
[Route("[controller]")]
public class AdminController(ISender sender) : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    #region Users
    [HttpGet("users")]
    public async Task<IActionResult> GetUsers(int page = 1)
    {
        var quantity = 10;
        var usersResult = await sender.Send(new GetUsersQuery(quantity, page));

        var viewModel = new GetUsersViewModel
        {
            Users = usersResult.Users,
            CurrentPage = page,
            LastPage = usersResult.LastPage
        };
        return View(viewModel);
    }

    [AuthorizeRole(Role.Author)]
    [HttpGet("users/create")]
    
    public IActionResult CreateUser()
    {
        var viewModel = new CreateUserViewModel();
        return View(viewModel);
    }

    [AuthorizeRole(Role.Author)]
    [HttpPost("users/create")]
    public async Task<IActionResult> CreateUser(CreateUserCommand command)
    {
        await sender.Send(command);
        return StatusCode(StatusCodes.Status201Created);
    }

    [AuthorizeRole(Role.Author)]
    [HttpGet("users/update/{id}")]
    public async Task<IActionResult> UpdateUser(int id)
    {
        var userResult = await sender.Send(new GetUserByIdQuery(id));

        var viewModel = new CreateUserViewModel
        {
            User = userResult.User
        };
        return View("CreateUser", viewModel);
    }

    [AuthorizeRole(Role.Author)]
    [HttpPost("users/update/{id}")]
    public async Task<IActionResult> UpdateUser(UpdateUserCommand command)
    {
        await sender.Send(command);
        return Ok();
    }

    [AuthorizeRole(Role.Author)]
    [HttpPost("users/delete")]
    public async Task<IActionResult> DeleteUser(DeleteUserCommand command)
    {
       await sender.Send(command);
       return RedirectToAction("GetUsers");
    }
    #endregion
    
    #region Tags
    [HttpGet("tags")]
    public async Task<IActionResult> GetTags(int page = 1)
    {
        var tagsResult = await sender.Send(new GetTagsQuery(10, page));

        var viewModel = new GetTagsViewModel
        {
            Tags = tagsResult.Tags,
            CurrentPage = page,
            LastPage = tagsResult.LastPage
        };
        return View(viewModel);
    }

    [AuthorizeRole(Role.Author)]
    [HttpGet("tags/create")]
    public IActionResult CreateTag()
    {
        var viewModel = new CreateTagViewModel();
        return View(viewModel);
    }

    [AuthorizeRole(Role.Author)]
    [HttpPost("tags/create")]
    public async Task<IActionResult> CreateTag(CreateTagCommand command)
    {
        await sender.Send(command);

        return StatusCode(StatusCodes.Status201Created);
    }
    
    [AuthorizeRole(Role.Author)]
    [HttpGet("tags/update/{slug}")]
    public async Task<IActionResult> UpdateTag(string slug)
    {
        var tagResult = await sender.Send(new GetTagBySlugQuery(slug));

        var viewModel = new CreateTagViewModel
        {
            Tag = tagResult.Tag
        };

        return View("CreateTag", viewModel);
    }

    [AuthorizeRole(Role.Author)]
    [HttpPost("tags/update")]
    public async Task<IActionResult> UpdateTag(UpdateTagCommand command)
    {
        await sender.Send(command);
        return Ok();
    }

    [AuthorizeRole(Role.Author)]
    [HttpPost("tags/delete")]
    public async Task<IActionResult> DeleteTag(DeleteTagCommand command)
    {
        try
        {
            await sender.Send(command);
            return RedirectToAction("GetTags");

        }
        catch (BadRequestException ex)
        {
            return RedirectToAction("GetTags", new { error = ex.Message });
        }
    }
    #endregion
    
    #region Posts
    [HttpGet("posts")]
    public async Task<IActionResult> GetPosts(int page = 1)
    {
        var postQuantity = 10;
        var postsResult = await sender.Send(new GetPostsQuery(10, page));

        var viewModel = new GetPostsViewModel
        {
            PostQuantity = postQuantity,
            Posts = postsResult.Posts,
            CurrentPage = page,
            LastPage = postsResult.LastPage
        };
        
        return View(viewModel);
    }

    [AuthorizeRole(Role.Author)]
    [HttpGet("posts/create")]
    public async Task<IActionResult> CreatePost()
    {
        var result = await sender.Send(new GetTagsQuery());
        var viewModel = new CreatePostViewModel
        {
            Tags = result.Tags
        };
        return View(viewModel);
    }

    [AuthorizeRole(Role.Author)]
    [HttpPost("posts/create")]
    public async Task<IActionResult> CreatePost([FromForm] CreatePostCommand command)
    {
        await sender.Send(command);
        return StatusCode(StatusCodes.Status201Created);
    }

    [AuthorizeRole(Role.Author)]
    [HttpGet("posts/update/{slug}")]
    public async Task<IActionResult> UpdatePost(string slug)
    {
        var postResult = await sender.Send(new GetPostQuery(slug));
        var tagsResult = await sender.Send(new GetTagsQuery());
        var slugs = postResult.Post.Tags.Select(tag => new
        {
            title = tag.Name, value = tag.Id
        });

        var viewModel = new CreatePostViewModel
        {
            Post = postResult.Post,
            Tags = tagsResult.Tags,
            SlugsJsonString = JsonSerializer.Serialize(slugs)
        };
        
        return View("CreatePost",viewModel);
    }

    [AuthorizeRole(Role.Author)]
    [HttpPost("posts/update")]
    public async Task<IActionResult> UpdatePost([FromForm] UpdatePostCommand command)
    {
        await sender.Send(command);
        return Ok();
    }

    [AuthorizeRole(Role.Author)]
    [HttpPost("posts/delete")]
    public async Task<IActionResult> DeletePost(DeletePostCommand command)
    {
        await sender.Send(command);
        return RedirectToAction("GetPosts");
    }
    #endregion
}