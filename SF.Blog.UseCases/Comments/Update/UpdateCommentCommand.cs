using Ardalis.Result;
using MediatR;
using SF.Blog.Core;

namespace SF.Blog.UseCases.Comments;
public record UpdateCommentCommand(IUserAuth User, string Id, string NewText) : IRequest<Result<Comment>>;
