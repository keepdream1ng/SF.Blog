using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SF.Blog.Infrastructure.Data;
using SF.Blog.Infrastructure.Data.DTO;

namespace SF.Blog.Infrastructure.Mediator.Queries;
public class GetCommentsByOwnerIdHandler(
	ApplicationDbContext dbContext
	) : IRequestHandler<GetCommentsByOwnerIdQuery, Result<ICollection<CommentDTO>>>
{
	public Task<Result<ICollection<CommentDTO>>> Handle(GetCommentsByOwnerIdQuery request, CancellationToken cancellationToken)
	{
		ICollection<CommentDTO> comments = dbContext.Comments
			.Include(c => c.Owner)
			.Where(c => c.OwnerId == request.Id)
			.OrderBy(c => c.Published)
			.Select(c => new CommentDTO(c.Id, c.ReplyToId, c.Owner.Name, c.Text))
			.ToList();
		return Task.FromResult(comments.Count > 0 ? Result.Success(comments) : Result.NotFound());
	}
}
