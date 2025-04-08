using Blog.Application.Core.Common;

namespace Blog.Application.Core.UseCases.Tags.Commands.UpdateTag;

public record UpdateTagCommand(int Id, string Name, FileUpload? Image) : IRequest;