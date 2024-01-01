using Ardalis.Result;
using MediatR;
using SF.Blog.Core;

namespace SF.Blog.UseCases.Comments;
public record CreateCommentCommand(IUserAuth Creator, string ReplyToId, string Text) : IRequest<Result<Comment>>;
