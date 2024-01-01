using Ardalis.Result;
using MediatR;
using SF.Blog.Core;

namespace SF.Blog.UseCases.Users;
public record GetUserByIdQuery (string Id) : IRequest<Result<User>>;
