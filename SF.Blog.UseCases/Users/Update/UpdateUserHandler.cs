using Ardalis.Result;
using MediatR;
using SF.Blog.Core;

namespace SF.Blog.UseCases.Users;
public class UpdateUserHandler(
	IAuthForManagerService AuthService,
	IMediator Mediator
	) : IRequestHandler<UpdateUserCommand, Result<User>>
{
	public async Task<Result<User>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
	{
		try
		{
			var userToUpdate = await Mediator.Send(new GetUserByIdQuery(request.Id));
			var manager = AuthService.GetManager(userToUpdate, request.User);
			return await manager.UpdateAsync(request.Name, request.About, request.DateOfBirth);
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
