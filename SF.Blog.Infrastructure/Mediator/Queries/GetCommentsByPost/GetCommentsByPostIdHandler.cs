using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SF.Blog.Infrastructure.Data;
using SF.Blog.Infrastructure.Data.DTO;

namespace SF.Blog.Infrastructure.Mediator.Queries;
public class GetCommentsByPostIdHandler(
	ApplicationDbContext DbContext
	) : IRequestHandler<GetCommentsByPostIdQuery, Result<ICollection<CommentDTO>>>
{
	public Task<Result<ICollection<CommentDTO>>> Handle(GetCommentsByPostIdQuery request, CancellationToken cancellationToken)
	{
		ICollection<CommentDTO> comments = DbContext.Comments
			.Include(c => c.Owner)
			.Where(c => c.ReplyToId == request.Id)
			.OrderBy(c => c.Published)
			.Select(c => new CommentDTO(c.Id, c.ReplyToId, c.Owner.Name, c.Text))
			.ToList();
		return Task.FromResult(comments.Count > 0 ? Result.Success(comments) : Result.NotFound());
	}
}
