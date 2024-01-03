using Ardalis.Result;
using MediatR;
using SF.Blog.Core;

namespace SF.Blog.UseCases.Users;
public class CreateUserHandler : IRequestHandler<CreateUserCommand, Result<User>>
{
	public async Task<Result<User>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
	{
		try
		{
			return new User(request.Name, request.About, request.DateOfBirth);
		}
		catch (Exception ex)
		{
			return Result.Error(ex.Message);
		}
	}
}
