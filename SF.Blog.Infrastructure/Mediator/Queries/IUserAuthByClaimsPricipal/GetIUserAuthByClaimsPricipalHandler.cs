using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SF.Blog.Core;
using SF.Blog.Infrastructure.Data.DTO;
using SF.Blog.Infrastructure.Data.Models;

namespace SF.Blog.Infrastructure.Mediator.Queries;
public class GetIUserAuthByClaimsPricipalHandler(
	UserManager<AppUserModel> UserManager
	) : IRequestHandler<GetIUserAuthByClaimsPricipalQuery, Result<IUserAuth>>
{
	public async Task<Result<IUserAuth>> Handle(GetIUserAuthByClaimsPricipalQuery request, CancellationToken cancellationToken)
	{
		var userModel = await UserManager.GetUserAsync(request.User);
		if (userModel is null) return Result.NotFound();
		var roles = await UserManager.GetRolesAsync(userModel);
		IReadOnlyCollection<Role> rolesCollection = roles.Select(role => new Role(role)).ToList();
		return Result.Success(new UserAuth(userModel.Id, rolesCollection) as IUserAuth);
	}
}
