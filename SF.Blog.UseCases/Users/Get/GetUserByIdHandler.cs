using Ardalis.Result;
using MediatR;
using SF.Blog.Core;

namespace SF.Blog.UseCases.Users.Get;
public class GetUserByIdHandler(IUserRepository Repo) : IRequestHandler<GetUserByIdQuery, Result<User>>
{
	public async Task<Result<User>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
	{
		var user = await Repo.GetByIdAsync(request.Id);
		return user is not null? Result<User>.Success(user) : Result<User>.NotFound();
	}
}
