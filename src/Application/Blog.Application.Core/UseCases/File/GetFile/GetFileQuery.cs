using Blog.Domain.ValueObjects;

namespace Blog.Application.Core.UseCases.File.GetFile;

public record GetFileQuery(string FileName):IRequest<GetFileResult>;

public record GetFileResult(BlobObject BlobObject);