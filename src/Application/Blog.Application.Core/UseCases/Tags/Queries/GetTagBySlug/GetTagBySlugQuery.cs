using Blog.Application.DTOs.Tag;

namespace Blog.Application.Core.UseCases.Tags.Queries.GetTagBySlug;

public record GetTagBySlugQuery(string Slug) : IRequest<GetTagBySlugResult>;

public record GetTagBySlugResult(TagDto Tag);