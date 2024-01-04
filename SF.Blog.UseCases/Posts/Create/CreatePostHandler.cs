using Ardalis.Result;
using MediatR;
using SF.Blog.Core;

namespace SF.Blog.UseCases.Posts;
public class CreatePostHandler(IPostRepository Repo) : IRequestHandler<CreatePostCommand, Result<Post>>
{
	public async Task<Result<Post>> Handle(CreatePostCommand request, CancellationToken cancellationToken)
	{
		try
		{
			Post newPost = new Post(request.Creator.Id, request.Title, request.Content);
			var result = await Repo.AddAsync(newPost);
			return newPost;
		}
		catch (Exception ex)
		{
			return Result<Post>.Error(ex.Message);
		}
	}
}
