using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SF.Blog.Infrastructure.Data;
using SF.Blog.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF.Blog.Infrastructure.Mediator.Queries.GetPostById;
public class GetPostModelByIdHandler(ApplicationDbContext DbContext) : IRequestHandler<GetPostModelByIdQuery, Result<PostModel>>
{
	public Task<Result<PostModel>> Handle(GetPostModelByIdQuery request, CancellationToken cancellationToken)
	{
		PostModel? post = DbContext.Posts
			.Include(p => p.Owner)
			.Include(p => p.Comments)
			.Include(p => p.Tags)
				.ThenInclude(tp => tp.Tag)
			.Where(p => p.Id == request.Id)
			.FirstOrDefault();
		return Task.FromResult((post is not null)? Result.Success(post) : Result.NotFound()); 
	}
}
