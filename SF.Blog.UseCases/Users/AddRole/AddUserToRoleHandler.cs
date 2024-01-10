using Ardalis.Result;
using MediatR;
using SF.Blog.Core;

namespace SF.Blog.UseCases.Users;
public class AddUserToRoleHandler(
	IAuthForManagerService AuthService,
	IMediator Mediator
	) : IRequestHandler<AddUserToRoleCommand, Result<bool>>
{
	public async Task<Result<bool>> Handle(AddUserToRoleCommand request, CancellationToken cancellationToken)
	{
		try
		{
			Result<User> userToUpdateResult = await Mediator.Send(new GetUserByIdQuery(request.Id));
			if (!userToUpdateResult.IsSuccess) return Result.NotFound();
			// Get manager for current object, if ownership or role doesnt support update - exception will be trown.
			var manager = AuthService.GetManager(userToUpdateResult.Value, request.User);
			bool result = await manager.AddRoleAsync(request.RoleName.Replace(" ", string.Empty));
			return result? Result.Success(result) : Result.Conflict();
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
