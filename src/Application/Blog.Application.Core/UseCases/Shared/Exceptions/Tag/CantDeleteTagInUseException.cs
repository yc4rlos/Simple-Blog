using Blog.Domain.Exceptions;

namespace Blog.Application.Core.UseCases.Shared.Exceptions.Tag;

public class CantDeleteTagInUseException(int id)
    : BadRequestException($"The tag with ID {id} can't be deleted because it is in use");