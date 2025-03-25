using Blog.Domain.Exceptions;

namespace Blog.Application.Core.UseCases.Shared.Exceptions.Post;

public class PostNotFoundException: NotFoundException
{
    public PostNotFoundException(string slug)
        : base($"The post with slug '{slug}' was not found")
    {
        
    }

    public PostNotFoundException(int id)
        : base($"The post with ID '{id}' was not found")
    {
        
    }
}