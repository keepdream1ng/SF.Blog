using Ardalis.Result;
using MediatR;
using SF.Blog.Core;

namespace SF.Blog.UseCases.Posts;
public class UpdatePostHandler(
	IMediator Mediator,
	IAuthForManagerService AuthService
	) : IRequestHandler<UpdatePostCommand, Result<Post>>
{
	public async Task<Result<Post>> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
	{
		try
		{
			var post = await Mediator.Send(new GetPostByIdQuery(request.Id));
			// Get manager for current object, if ownership or role doesnt support update - exception will be trown.
			var manager = AuthService.GetManager(post, request.User);
			return await manager.UpdatePostAsync(request.Title, request.Content);
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
