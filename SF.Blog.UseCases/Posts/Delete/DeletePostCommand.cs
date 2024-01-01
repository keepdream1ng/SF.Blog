using Ardalis.Result;
using MediatR;
using SF.Blog.Core;

namespace SF.Blog.UseCases.Posts;
public record DeletePostCommand(IUserAuth User, string Id) : IRequest<Result<bool>>;
