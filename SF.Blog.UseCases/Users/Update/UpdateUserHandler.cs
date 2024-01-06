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
			Result<User> userToUpdateResult = await Mediator.Send(new GetUserByIdQuery(request.Id));
			if (!userToUpdateResult.IsSuccess) return Result.NotFound();
			var manager = AuthService.GetManager(userToUpdateResult.Value, request.User);
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
