namespace Blog.Application.Core.UseCases.User.Commands.Login;

internal class LoginCommandHandler
: IRequestHandler<LoginCommand, LoginCommandResult>
{
    public Task<LoginCommandResult> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}