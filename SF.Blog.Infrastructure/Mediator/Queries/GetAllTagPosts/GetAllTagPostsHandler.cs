using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SF.Blog.Infrastructure.Data;
using SF.Blog.Infrastructure.Data.DTO;

namespace SF.Blog.Infrastructure.Mediator.Queries.GetAllTagPosts;
public class GetAllTagPostsHandler (ApplicationDbContext DbContext) : IRequestHandler<GetAllTagPostsQuery, Result<ICollection<TagPostDTO>>>
{
	public Task<Result<ICollection<TagPostDTO>>> Handle(GetAllTagPostsQuery request, CancellationToken cancellationToken)
	{
		ICollection<TagPostDTO> tagposts = DbContext.TagPosts
			.Include(tp => tp.Post)
			.Include(tp => tp.Tag)
			.Select(tp => new TagPostDTO(tp.Tag.Value, tp.PostId, tp.Post.Title))
			.ToList();
		return Task.FromResult(tagposts.Count > 0 ? Result.Success(tagposts) : Result.NotFound());
	}
}
