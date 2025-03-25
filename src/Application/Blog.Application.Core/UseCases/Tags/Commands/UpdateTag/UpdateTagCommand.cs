using Microsoft.AspNetCore.Http;

namespace Blog.Application.Core.UseCases.Tags.Commands.UpdateTag;

public record UpdateTagCommand(int Id, string Name, IFormFile? Image) : IRequest;