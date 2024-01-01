using Ardalis.Result;
using MediatR;
using SF.Blog.Core;

namespace SF.Blog.UseCases.Comments;
public class CreateCommentHandler(IRepository<Comment> Repo) : IRequestHandler<CreateCommentCommand, Result<Comment>>
{
	public async Task<Result<Comment>> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
	{
		try
		{
			Comment newComment = new Comment(request.Creator.Id, request.ReplyToId, request.Text);
			await Repo.AddAsync(newComment);
			return newComment;
		}
		catch (Exception ex)
		{
			return Result<Comment>.Error(ex.Message);
		}
	}
}
