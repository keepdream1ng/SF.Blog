using Ardalis.Result;
using MediatR;
using SF.Blog.Core;

namespace SF.Blog.UseCases.Posts;
public record GetAllPostsQuery() : IRequest<Result<IEnumerable<Post>>>;
