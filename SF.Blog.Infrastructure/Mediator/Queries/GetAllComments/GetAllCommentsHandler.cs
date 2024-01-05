using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SF.Blog.Infrastructure.Data;
using SF.Blog.Infrastructure.Data.DTO;

namespace SF.Blog.Infrastructure.Mediator.Queries.GetAllComments;
public class GetAllCommentsHandler (ApplicationDbContext DbContext) : IRequestHandler<GetAllCommentsQuery, Result<ICollection<CommentDTO>>>
{
	public Task<Result<ICollection<CommentDTO>>> Handle(GetAllCommentsQuery request, CancellationToken cancellationToken)
	{
		ICollection<CommentDTO> comments = DbContext.Comments
			.Include(c => c.Owner)
			.Select(c => new CommentDTO(c.Id, c.ReplyToId, c.Owner.Name, c.Text))
			.ToList();
		return Task.FromResult(comments.Count > 0 ? Result.Success(comments) : Result.NotFound());
	}
}
