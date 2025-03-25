using Blog.Application.DTOs.Post;
using Blog.Application.DTOs.User;

namespace Blog.Presentation.Web.ViewModels.Author;

public class AuthorViewModel
{
    public required UserAuthorDto Author { get; set; }
    public required IEnumerable<PostDto> Posts { get; set; }
}