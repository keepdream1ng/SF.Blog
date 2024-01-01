﻿using Ardalis.Result;
using MediatR;
using SF.Blog.Core;

namespace SF.Blog.UseCases.Comments;
public class DeleteCommentHandler(
	IRepository<Comment> Repo,
	IAuthForManagerService AuthService
	) : IRequestHandler<DeleteCommentCommand, Result<bool>>
{
	public async Task<Result<bool>> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
	{
		try
		{
			var comment = await Repo.GetByIdAsync(request.Id);
			// Get manager for current object, if ownership or role doesnt support delete - exception will be trown.
			var manager = AuthService.GetManager(comment, request.User);
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
