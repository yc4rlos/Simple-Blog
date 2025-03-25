using Blog.Application.DTOs.User;

namespace Blog.Application.Core.UseCases.User.Queries.GetUsers;

public record GetUsersQuery(int Quantity, int Page): IRequest<GetUsersQueryResult>;

public record GetUsersQueryResult(IEnumerable<UserDto> Users, int TotalCount, int LastPage);