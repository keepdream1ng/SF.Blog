using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SF.Blog.Infrastructure.Data;
using SF.Blog.Infrastructure.Data.DTO;

namespace SF.Blog.Infrastructure.Mediator.Queries;
public class GetPostsByOwnerIdHandler(ApplicationDbContext DbContext) : IRequestHandler<GetPostsByOwnerIdQuery, Result<ICollection<PostDTO>>>
{
	public Task<Result<ICollection<PostDTO>>> Handle(GetPostsByOwnerIdQuery request, CancellationToken cancellationToken)
	{
		ICollection<PostDTO> posts = DbContext.Posts
			.Include(p => p.Owner)
			.Include(p => p.Tags)
				.ThenInclude(tp => tp.Tag)
			.Where(p => p.OwnerId == request.Id)
			.Select(p => new PostDTO(p.Id, p.Owner.Name, p.Title, p.Content, String.Join(' ', p.Tags.Select(tp => tp.Tag.Value))))
			.ToList();

		return Task.FromResult(posts.Count > 0? Result.Success(posts): Result.NotFound());
	}
}
