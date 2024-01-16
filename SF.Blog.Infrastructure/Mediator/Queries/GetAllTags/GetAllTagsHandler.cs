using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SF.Blog.Infrastructure.Data;
using SF.Blog.Infrastructure.Data.DTO;

namespace SF.Blog.Infrastructure.Mediator.Queries;
public class GetAllTagsHandler(
	ApplicationDbContext dbContext
	) : IRequestHandler<GetAllTagsQuery, Result<ICollection<TagDTO>>>
{
	public Task<Result<ICollection<TagDTO>>> Handle(GetAllTagsQuery request, CancellationToken cancellationToken)
	{
		ICollection<TagDTO> tags = dbContext.Tags
			.Include(t => t.Posts)
			.Where(t => t.Posts.Count() > 0)
			.Select(t => new TagDTO(t.Value, t.Posts.Count()))
			.ToList();

		return Task.FromResult(tags.Count > 0 ? Result.Success(tags) : Result.NotFound());
	}
}
