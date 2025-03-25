using Blog.Application.DTOs.Post;

namespace Blog.Application.Core.UseCases.Posts.Queries.GetPostsByTagQuery;

public record GetPostsByTagQuery(string TagSlug, int Quantity = int.MaxValue): IRequest<GetPostsByTagResult>;

public record GetPostsByTagResult(IEnumerable<PostDto> Posts);