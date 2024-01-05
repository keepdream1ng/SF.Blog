using Ardalis.Result;
using MediatR;
using SF.Blog.Core;

namespace SF.Blog.UseCases.Comments;
public class UpdateCommentHandler(
	IMediator Mediator,
	IAuthForManagerService AuthService
	) : IRequestHandler<UpdateCommentCommand, Result<Comment>>
{
	public async Task<Result<Comment>> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
	{
		try
		{
			Result<Comment> result = await Mediator.Send(new GetCommentByIdQuery(request.Id));
			if (!result.IsSuccess) return Result.NotFound(); 
			// Get manager for current object, if ownership or role doesnt support update - exception will be trown.
			var manager = AuthService.GetManager(result.Value, request.User);
			return await manager.UpdateCommentAsync(request.NewText);
		}
		catch (UserAccessDeniedException)
		{
			return Result<Comment>.Unauthorized();
		}
		catch (Exception ex)
		{
			return Result<Comment>.Error(ex.Message);
		}
	}
}
