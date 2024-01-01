using Ardalis.Result;
using MediatR;
using SF.Blog.Core;

namespace SF.Blog.UseCases.Users;
public class DeleteUserHandler(
	IAuthForManagerService AuthService,
	IMediator Mediator
	) : IRequestHandler<DeleteUserCommand, Result<bool>>
{
	public async Task<Result<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
	{
		try
		{
			var userToUpdate = await Mediator.Send(new GetUserByIdQuery(request.Id));
			var manager = AuthService.GetManager(userToUpdate, request.User);
			return await manager.DeleteAsync();
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
