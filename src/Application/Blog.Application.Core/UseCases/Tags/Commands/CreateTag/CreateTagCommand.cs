using Microsoft.AspNetCore.Http;

namespace Blog.Application.Core.UseCases.Tags.Commands.CreateTag;

public record CreateTagCommand(string Name, IFormFile Image): IRequest;
