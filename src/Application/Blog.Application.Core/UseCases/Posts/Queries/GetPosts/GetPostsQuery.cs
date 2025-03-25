using Blog.Application.DTOs.Post;

namespace Blog.Application.Core.UseCases.Posts.Queries.GetPosts;

public record GetPostsQuery(
    int Quantity = int.MaxValue,
    int Page = 1,
    bool OnlyPosted = false,
    string? SearchTerm = null
    ): IRequest<GetPostsQueryResult>;

public record GetPostsQueryResult(IEnumerable<PostDto> Posts, int LastPage);