using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SF.Blog.Infrastructure.Data;
using SF.Blog.Infrastructure.Data.DTO;

namespace SF.Blog.Infrastructure.Mediator.Queries;
public class GetPostsByTagHandler(
    ApplicationDbContext dbContext
    ) : IRequestHandler<GetPostsByTagQuery, Result<ICollection<PostDTO>>>
{
    public Task<Result<ICollection<PostDTO>>> Handle(GetPostsByTagQuery request, CancellationToken cancellationToken)
    {
        Data.Models.TagModel? checkedTag = dbContext.Tags
			.Where(t => t.Value == request.Tag)
			.FirstOrDefault();

		if (checkedTag is null) return Task.FromResult(Result<ICollection<PostDTO>>.NotFound());

		ICollection<PostDTO> posts = dbContext.Posts
			.Include(p => p.Owner)
			.Include(p => p.Tags)
				.ThenInclude(tp => tp.Tag)
			.Where(p => p.Tags.Any(tp => tp.TagId == checkedTag.Id))
			.OrderByDescending(p => p.Published)
			.Select(p => new PostDTO(p.Id, p.Owner.Name, p.Title, p.Content, String.Join(' ', p.Tags.Select(tp => tp.Tag.Value))))
			.ToList();

		return Task.FromResult(posts.Count > 0? Result.Success(posts): Result.NotFound());
    }
}
