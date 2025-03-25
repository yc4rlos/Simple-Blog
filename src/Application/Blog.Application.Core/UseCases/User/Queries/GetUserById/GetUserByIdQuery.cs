using Blog.Application.DTOs.User;

namespace Blog.Application.Core.UseCases.User.Queries.GetUserById;

public record GetUserByIdQuery(int Id): IRequest<GetUserByIdResult>;

public record GetUserByIdResult(UserCompleteDto User);