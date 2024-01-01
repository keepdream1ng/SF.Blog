using Ardalis.Result;
using MediatR;
using SF.Blog.Core;

namespace SF.Blog.UseCases.Posts;
public record UpdatePostCommand(IUserAuth User, string Id, string Title, string Content) : IRequest<Result<Post>>;
