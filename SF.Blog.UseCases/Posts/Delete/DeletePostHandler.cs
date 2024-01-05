using Ardalis.Result;
using MediatR;
using SF.Blog.Core;

namespace SF.Blog.UseCases.Posts;
public class DeletePostHandler(
	IMediator Mediator,
	IAuthForManagerService AuthService
	) : IRequestHandler<DeletePostCommand, Result<bool>>
{
	public async Task<Result<bool>> Handle(DeletePostCommand request, CancellationToken cancellationToken)
	{
		try
		{
			Result<Post> post = await Mediator.Send(new GetPostByIdQuery(request.Id));
			if (!post.IsSuccess) return Result.NotFound();
			// Get manager for current object, if ownership or role doesnt support delete - exception will be trown.
			var manager = AuthService.GetManager(post, request.User);
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
