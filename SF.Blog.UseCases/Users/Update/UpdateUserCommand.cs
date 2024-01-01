using Ardalis.Result;
using MediatR;
using SF.Blog.Core;

namespace SF.Blog.UseCases.Users;
public record UpdateUserCommand(IUserAuth User, string Id, string Name, string About, DateTime DateOfBirth) : IRequest<Result<User>>;
