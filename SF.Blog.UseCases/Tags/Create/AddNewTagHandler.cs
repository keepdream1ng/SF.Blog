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
			Result<Post> postResult = await Mediator.Send(new GetPostByIdQuery(request.PostId));
			if (!postResult.IsSuccess) return Result.Invalid();
			// Get manager for current object, if ownership or role doesnt support update - exception will be trown.
			var manager = AuthService.GetManager(postResult, request.User);
			bool tagAdded = await manager.AddTagAsync(request.Tag.Replace(" ", string.Empty).ToLower());
			return tagAdded ? Result.Success(tagAdded) : Result.Invalid();
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
