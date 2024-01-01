using Ardalis.Result;
using MediatR;
using SF.Blog.Core;

namespace SF.Blog.UseCases.Comments;
public record DeleteCommentCommand(IUserAuth User, string Id) : IRequest<Result<bool>>;
