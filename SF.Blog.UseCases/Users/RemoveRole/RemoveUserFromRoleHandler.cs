using Ardalis.Result;
using MediatR;
using SF.Blog.Core;

namespace SF.Blog.UseCases.Users;
public class RemoveUserFromRoleHandler(
	IAuthForManagerService AuthService,
	IMediator Mediator
	) : IRequestHandler<RemoveUserFromRoleCommand, Result<bool>>
{
	public async Task<Result<bool>> Handle(RemoveUserFromRoleCommand request, CancellationToken cancellationToken)
	{
		try
		{
			Result<User> userToUpdateResult = await Mediator.Send(new GetUserByIdQuery(request.Id));
			if (!userToUpdateResult.IsSuccess) return Result.NotFound();
			// Get manager for current object, if ownership or role doesnt support update - exception will be trown.
			var manager = AuthService.GetManager(userToUpdateResult.Value, request.User);
			bool result = await manager.RemoveRoleAsync(new Role(request.RoleName));
			return result? Result.Success(result) : Result.NotFound();
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
