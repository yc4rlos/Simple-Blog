using Blog.Application.DTOs.Tag;

namespace Blog.Application.Core.UseCases.Tags.Queries.GetTags;

public record GetTagsQuery(
    int Quantity = int.MaxValue,
    int Page = 1,
    bool IncludeEmpty = true,
    string? SearchTerm = null,
    bool CountOnlyPostedPosts = false
    ): IRequest<GetTagsResult>;

public record GetTagsResult(IEnumerable<TagDto> Tags, int TotalCount, int LastPage);