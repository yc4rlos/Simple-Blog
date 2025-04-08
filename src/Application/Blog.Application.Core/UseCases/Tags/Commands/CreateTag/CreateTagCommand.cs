using Blog.Application.Core.Common;

namespace Blog.Application.Core.UseCases.Tags.Commands.CreateTag;

public record CreateTagCommand(string Name, FileUpload Image) : IRequest;
