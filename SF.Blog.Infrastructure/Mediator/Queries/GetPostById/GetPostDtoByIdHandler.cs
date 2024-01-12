using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SF.Blog.Infrastructure.Data;
using SF.Blog.Infrastructure.Data.DTO;

namespace SF.Blog.Infrastructure.Mediator.Queries;
public class GetPostDtoByIdHandler(ApplicationDbContext DbContext) : IRequestHandler<GetPostDtoByIdQuery, Result<PostDTO>>
{
	public Task<Result<PostDTO>> Handle(GetPostDtoByIdQuery request, CancellationToken cancellationToken)
	{
		var post = DbContext.Posts
			.Include(p => p.Owner)
			.Include(p => p.Tags)
				.ThenInclude(tp => tp.Tag)
			.Where(p => p.Id == request.Id)
			.FirstOrDefault();

		if (post is null)
		{
			return Task.FromResult(Result<PostDTO>.NotFound());
		}

		var postDto = new PostDTO(post.Id, post.Owner.Name, post.Title, post.Content, String.Join(' ', post.Tags.Select(tp => tp.Tag.Value)));
		return Task.FromResult(Result.Success(postDto));
	}
}
