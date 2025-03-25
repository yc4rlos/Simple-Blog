using Blog.Application.DTOs.Post;

namespace Blog.Application.Core.UseCases.Posts.Queries.GetPostsByUserSlug;

public record GetPostsByUserQuery(int UserId, int Take = int.MaxValue) : IRequest<GetPostsByUserResult>;

public record GetPostsByUserResult(IEnumerable<PostDto> Posts);