using Ardalis.Result;
using MediatR;
using SF.Blog.Core;

namespace SF.Blog.UseCases.Posts;
public class GetPostByIdHanlder(IRepository<Post> Repo) : IRequestHandler<GetPostByIdQuery, Result<Post>>
{
	public async Task<Result<Post>> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
	{
		var post = await Repo.GetByIdAsync(request.Id);
		return post is not null? Result<Post>.Success(post) : Result<Post>.NotFound();
	}
}
