using Ardalis.Result;
using MediatR;
using SF.Blog.Core;
using SF.Blog.UseCases.Posts;

namespace SF.Blog.UseCases.Tags;
public class AddNewTagHandler(
	IAuthForManagerService AuthService,
	IMediator Mediator
	) : IRequestHandler<AddNewTagCommand, Result<bool>>
{
	public async Task<Result<bool>> Handle(AddNewTagCommand request, CancellationToken cancellationToken)
	{
		try
		{
			var post = await Mediator.Send(new GetPostByIdQuery(request.PostId));
			// Get manager for current object, if ownership or role doesnt support update - exception will be trown.
			var manager = AuthService.GetManager(post, request.User);
			return await manager.AddTagAsync(request.Tag);
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
