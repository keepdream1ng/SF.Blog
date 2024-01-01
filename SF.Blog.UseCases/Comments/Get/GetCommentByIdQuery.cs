using Ardalis.Result;
using MediatR;
using SF.Blog.Core;

namespace SF.Blog.UseCases.Comments;
public record GetCommentByIdQuery(string Id) : IRequest<Result<Comment>>;
