using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SF.Blog.Infrastructure.Data;
using SF.Blog.Infrastructure.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF.Blog.Infrastructure.Mediator.Queries.GetCommentById;
public class GetCommentDtoByIdHandler(
	ApplicationDbContext dbContext
	) : IRequestHandler<GetCommentDtoByIdQuery, Result<CommentDTO>>
{
	public Task<Result<CommentDTO>> Handle(GetCommentDtoByIdQuery request, CancellationToken cancellationToken)
	{
		var comment = dbContext.Comments
			.Include(c => c.Owner)
			.Where(c => c.Id == request.Id)
			.FirstOrDefault();

		if (comment is null) return Task.FromResult(Result<CommentDTO>.NotFound());
		return Task.FromResult(Result.Success(new CommentDTO(comment.Id, comment.ReplyToId, comment.Owner.Name, comment.Text)));
	}
}
