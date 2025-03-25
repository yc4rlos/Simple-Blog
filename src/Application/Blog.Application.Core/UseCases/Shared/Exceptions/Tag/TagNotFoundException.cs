using Blog.Domain.Exceptions;

namespace Blog.Application.Core.UseCases.Shared.Exceptions.Tag;

public class TagNotFoundException : NotFoundException
{
    public TagNotFoundException(string slug) : base($"The tag with slug {slug} was not found")
    {
    }

    public TagNotFoundException(int id) : base($"The tag with ID {id} was not found")
    {
    }
}