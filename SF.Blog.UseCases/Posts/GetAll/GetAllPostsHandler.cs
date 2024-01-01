using Ardalis.Result;
using MediatR;
using SF.Blog.Core;

namespace SF.Blog.UseCases.Posts;
public class GetAllPostsHandler(IRepository<Post> Repo) : IRequestHandler<GetAllPostsQuery, Result<IEnumerable<Post>>>
{
	public async Task<Result<IEnumerable<Post>>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
	{
		return await Repo.ListAsync();
	}
}
