using Ardalis.Result;
using MediatR;
using SF.Blog.Core;

namespace SF.Blog.UseCases.Comments;
public class GetAllCommentsHandler(IRepository<Comment> Repo) : IRequestHandler<GetAllCommentsQuery, Result<IEnumerable<Comment>>>
{
	public async Task<Result<IEnumerable<Comment>>> Handle(GetAllCommentsQuery request, CancellationToken cancellationToken)
	{
		return await Repo.ListAsync(cancellationToken);
	}
}
