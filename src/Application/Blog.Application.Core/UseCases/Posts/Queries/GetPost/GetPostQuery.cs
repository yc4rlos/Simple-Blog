using Blog.Application.DTOs.Post;

namespace Blog.Application.Core.UseCases.Posts.Queries.GetPost;

public record GetPostQuery(string Slug): IRequest<GetPostQueryResult>;

public record GetPostQueryResult(PostCompleteDto Post);