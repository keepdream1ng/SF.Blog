using Ardalis.Result;
using MediatR;
using SF.Blog.Core;

namespace SF.Blog.UseCases.Posts;
public record CreatePostCommand(IUserAuth Creator, string Title, string Content) : IRequest<Result<Post>>;
