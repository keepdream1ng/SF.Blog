using Ardalis.Result;
using MediatR;
using SF.Blog.Core;

namespace SF.Blog.UseCases.Comments;
public record GetAllCommentsQuery() : IRequest<Result<IEnumerable<Comment>>>;
