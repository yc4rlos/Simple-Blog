using Blog.Application.DTOs.Post;
using Blog.Application.DTOs.User;

namespace Blog.Application.Core.UseCases.User.Queries.GetUserAuthorBySlug;

public record GetUserAuthorBySlugQuery(string Slug) : IRequest<GetUserAuthorBySlugResponse>;

public record GetUserAuthorBySlugResponse(UserAuthorDto UserAuthorDto);