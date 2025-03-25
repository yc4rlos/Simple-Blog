using Blog.Application.Core.Helpers;
using Blog.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Blog.Application.Core.UseCases.Posts.Commands.UpdatePost;

public record UpdatePostCommand(
    int Id,
    string Title,
    string Content,
    string Summary,
    DateTime PostDate,
    List<int> Tags,
    IFormFile? Image) : IRequest;


public static class UpdatePostCommandExtension
{
    public static Post ToEntity(this UpdatePostCommand command, Post entity)
    {
        entity.Title = command.Title.Trim();
        entity.Slug = SlugHelper.CreateSlug(command.Title);
        entity.Content = command.Content.Trim().Replace(@"&nbsp;", " ");
        entity.Summary = command.Summary.Trim();
        entity.PostDate = command.PostDate;
        entity.Tags = command.Tags.Select(x => new PostTag { TagId = x }).ToList();
        return entity;
    }
}