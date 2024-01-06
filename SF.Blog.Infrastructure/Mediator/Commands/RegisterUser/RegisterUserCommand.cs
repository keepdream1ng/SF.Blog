using Ardalis.Result;
using MediatR;
using SF.Blog.Core;

namespace SF.Blog.Infrastructure.Mediator.Commands;
public record RegisterUserCommand (string Email, string Password, string Name, string About, DateTime DateOfBirth) : IRequest<Result<User>>;
