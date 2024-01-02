using Ardalis.Result;
using MediatR;
using SF.Blog.Core;

namespace SF.Blog.UseCases.Users;
public record AddUserToRoleCommand(IUserAuth User, string Id, string RoleName) : IRequest<Result<bool>>;
