﻿using Ardalis.Result;
using MediatR;
using SF.Blog.Core;
using SF.Blog.UseCases.Posts;

namespace SF.Blog.UseCases.Tags;
public class RemoveTagHandler(
	IAuthForManagerService AuthService,
	IMediator Mediator
	) : IRequestHandler<RemoveTagCommand, Result<bool>>
{
	public async Task<Result<bool>> Handle(RemoveTagCommand request, CancellationToken cancellationToken)
	{
		try
		{
			Result<Post> postResult = await Mediator.Send(new GetPostByIdQuery(request.PostId));
			if (!postResult.IsSuccess) return Result.Invalid();
			// Get manager for current object, if ownership or role doesnt support update - exception will be trown.
			var manager = AuthService.GetManager(postResult.Value, request.User);
			bool tagRemoved = await manager.RemoveTagAsync(new Tag(request.Tag));
			return tagRemoved? Result.Success(tagRemoved) : Result.Invalid();
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
