using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SF.Blog.Infrastructure.Data;
using SF.Blog.Infrastructure.Data.DTO;

namespace SF.Blog.Infrastructure.Mediator.Queries;
public class GetAllPostsHandler(ApplicationDbContext DbContext) : IRequestHandler<GetAllPostsQuery, Result<ICollection<PostDTO>>>
{
	public Task<Result<ICollection<PostDTO>>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
	{
		ICollection<PostDTO> posts = DbContext.Posts
			.Include(p => p.Owner)
			.Select(p => new PostDTO(p.Id, p.Owner.Name, p.Title, p.Content))
			.ToList();

		return Task.FromResult(posts.Count > 0? Result.Success(posts): Result.NotFound());
	}
}
