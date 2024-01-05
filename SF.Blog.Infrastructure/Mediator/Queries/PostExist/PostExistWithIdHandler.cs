using Ardalis.Result;
using MediatR;
using SF.Blog.Infrastructure.Data;

namespace SF.Blog.Infrastructure.Mediator.Queries.PostExist;
public class PostExistWithIdHandler(ApplicationDbContext DbContext) : IRequestHandler<PostExistWithIdQuery, Result<bool>>
{
	public Task<Result<bool>> Handle(PostExistWithIdQuery request, CancellationToken cancellationToken)
	{
		var exist = DbContext.Posts.Any(p => p.Id == request.Id);
		return Task.FromResult(exist? Result.Success(exist): Result.NotFound());
	}
}
