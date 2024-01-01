using Ardalis.Result;
using MediatR;
using SF.Blog.Core;

namespace SF.Blog.UseCases.Comments.Update;
public class UpdateCommentHandler(
	IRepository<Comment> Repo,
	IAuthForManagerService AuthService
	) : IRequestHandler<UpdateCommentCommand, Result<Comment>>
{
	public async Task<Result<Comment>> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
	{
		try
		{
			var comment = await Repo.GetByIdAsync(request.Id);
			// Get manager for current object, if ownership or role doesnt support update - exception will be trown.
			var manager = AuthService.GetManager(comment, request.User);
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
