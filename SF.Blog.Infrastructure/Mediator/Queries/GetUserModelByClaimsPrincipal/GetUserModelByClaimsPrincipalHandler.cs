using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SF.Blog.Infrastructure.Data.Models;

namespace SF.Blog.Infrastructure.Mediator.Queries;
public class GetUserModelByClaimsPrincipalHandler(
	UserManager<AppUserModel> UserManager
	) : IRequestHandler<GetUserModelByClaimsPrincipalQuery, Result<AppUserModel>>
{
	public async Task<Result<AppUserModel>> Handle(GetUserModelByClaimsPrincipalQuery request, CancellationToken cancellationToken)
	{
		AppUserModel? user = await UserManager.GetUserAsync(request.User);
		if (user is null) return Result.NotFound();
		return Result.Success(user);
	}
}
