using Ardalis.Result;
using MediatR;
using SF.Blog.Core;

namespace SF.Blog.UseCases.Users;
public record CreateUserCommand(string Name, string About, DateTime DateOfBirth) : IRequest<Result<User>>;
