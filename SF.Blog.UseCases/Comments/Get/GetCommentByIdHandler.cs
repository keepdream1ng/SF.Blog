using Ardalis.Result;
using MediatR;
using SF.Blog.Core;

namespace SF.Blog.UseCases.Comments;
public class GetCommentByIdHandler(IRepository<Comment> Repo) : IRequestHandler<GetCommentByIdQuery, Result<Comment>>
{
	public async Task<Result<Comment>> Handle(GetCommentByIdQuery request, CancellationToken cancellationToken)
	{
		var comment = await Repo.GetByIdAsync(request.Id);
		return comment is not null? Result.Success(comment) : Result.NotFound();
	}
}
