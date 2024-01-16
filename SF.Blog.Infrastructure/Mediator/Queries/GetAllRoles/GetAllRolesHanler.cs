using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SF.Blog.Infrastructure.Data;
using SF.Blog.Infrastructure.Data.DTO;
using SF.Blog.Infrastructure.Data.Models;

namespace SF.Blog.Infrastructure.Mediator.Queries;
public class GetAllRolesHanler(
    UserManager<AppUserModel> userManager,
    RoleManager<IdentityRole> roleManager
    ) : IRequestHandler<GetAllRolesQuery, Result<ICollection<RoleDTO>>>
{
    public async Task<Result<ICollection<RoleDTO>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        List<IdentityRole> roles = await roleManager.Roles.ToListAsync(cancellationToken);
        ICollection<RoleDTO> rolesResult = new List<RoleDTO>();

        foreach (IdentityRole role in roles)
        {
            IList<AppUserModel> usersInRole = await userManager.GetUsersInRoleAsync(role.Name);
            rolesResult.Add(new RoleDTO(role.Name, usersInRole.Count));
        }

        if (rolesResult.Count > 0)
        {
            return Result.Success(rolesResult);
        }
        else
        {
            return Result.NotFound();
        }
    }
}
