using Blog.Application.DTOs.Tag;

namespace Blog.Application.Core.UseCases.Tags.Queries.GetTag;

public record GetTagQuery(int Id): IRequest<GetTagQueryResult>;

public record GetTagQueryResult(TagDto Tag);