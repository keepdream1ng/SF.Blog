using Ardalis.Result;
using MediatR;
using SF.Blog.Core;
using SF.Blog.Infrastructure.Data;
using SF.Blog.Infrastructure.Data.DTO;

namespace SF.Blog.Infrastructure.Mediator.Queries;
public class GetAllUsersHandler(ApplicationDbContext DbContext) : IRequestHandler<GetAllUsersQuery, Result<ICollection<UserDTO>>>
{
	public Task<Result<ICollection<UserDTO>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
	{
		ICollection<UserDTO> users = DbContext.Users
			.Select(u => new UserDTO(u.Id, u.Email))
			.ToList();
		return Task.FromResult(users.Count > 0? Result.Success(users) : Result.NotFound());
	}
}
