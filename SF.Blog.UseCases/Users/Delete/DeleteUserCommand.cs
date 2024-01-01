using Ardalis.Result;
using MediatR;
using SF.Blog.Core;

namespace SF.Blog.UseCases.Users;
public record DeleteUserCommand(IUserAuth User, string Id) : IRequest<Result<bool>>;
