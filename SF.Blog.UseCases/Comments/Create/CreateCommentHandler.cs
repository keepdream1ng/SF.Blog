using Ardalis.Result;
using MediatR;
using SF.Blog.Core;

namespace SF.Blog.UseCases.Comments;
public class CreateCommentHandler(
	IRepository<Comment> Repo,
	IMediator Mediator
	) : IRequestHandler<CreateCommentCommand, Result<Comment>>
{
	public async Task<Result<Comment>> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
	{
		try
		{
			Comment newComment = new Comment(request.Creator.Id, request.ReplyToId, request.Text);
			Comment result = await Repo.AddAsync(newComment);
			return result;
		}
		catch (Exception ex)
		{
			return Result<Comment>.Error(ex.Message);
		}
	}
}
