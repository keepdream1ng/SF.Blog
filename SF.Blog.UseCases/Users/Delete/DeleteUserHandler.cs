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
			Result<User> userToUpdateResult = await Mediator.Send(new GetUserByIdQuery(request.Id));
			if (!userToUpdateResult.IsSuccess) return Result.NotFound();
			var manager = AuthService.GetManager(userToUpdateResult.Value, request.User);
			bool deleteResult = await manager.DeleteAsync();
			return deleteResult? Result.Success(deleteResult) : Result.Invalid();
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
