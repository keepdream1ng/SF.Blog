using Ardalis.Result;
using MediatR;
using SF.Blog.Core;

namespace SF.Blog.UseCases.Users;
public class CreateUserHandler(
	IUserRepository Repo
	) : IRequestHandler<CreateUserCommand, Result<User>>
{
	public async Task<Result<User>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
	{
		try
		{
			var user = new User(request.Name, request.About, request.DateOfBirth);
			return await Repo.AddAsync(user);
		}
		catch (UserAccessDeniedException)
		{
			return Result.Unauthorized();
		}
		catch (Exception ex)
		{
			return Result.Error(ex.Message);
		}
	}
}
