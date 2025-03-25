namespace Blog.Application.Core.UseCases.Posts.Commands.DeletePost;

public record DeletePostCommand(int Id): IRequest;
