using Ardalis.Result;
using MediatR;
using SF.Blog.Core;

namespace SF.Blog.UseCases.Comments;
public class DeleteCommentHandler(
	IMediator Mediator,
	IAuthForManagerService AuthService
	) : IRequestHandler<DeleteCommentCommand, Result<bool>>
{
	public async Task<Result<bool>> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
	{
		try
		{
			Result<Comment> result = await Mediator.Send(new GetCommentByIdQuery(request.Id));
			if (!result.IsSuccess) return Result.NotFound(); 
			// Get manager for current object, if ownership or role doesnt support delete - exception will be trown.
			var manager = AuthService.GetManager(result.Value, request.User);
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
