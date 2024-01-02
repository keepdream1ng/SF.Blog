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
			var userToUpdate = await Mediator.Send(new GetUserByIdQuery(request.Id));
			// Get manager for current object, if ownership or role doesnt support update - exception will be trown.
			var manager = AuthService.GetManager(userToUpdate, request.User);
			return await manager.AddRoleAsync(request.RoleName);
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
