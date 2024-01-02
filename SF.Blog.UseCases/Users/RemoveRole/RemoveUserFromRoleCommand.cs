using Ardalis.Result;
using MediatR;
using SF.Blog.Core;

namespace SF.Blog.UseCases.Users;
public record RemoveUserFromRoleCommand(IUserAuth User, string Id, string RoleName) : IRequest<Result<bool>>;
