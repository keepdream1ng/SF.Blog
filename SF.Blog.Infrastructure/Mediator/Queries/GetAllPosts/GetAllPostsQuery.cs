using Ardalis.Result;
using MediatR;
using SF.Blog.Infrastructure.Data.DTO;

namespace SF.Blog.Infrastructure.Mediator.Queries;
public record GetAllPostsQuery() : IRequest<Result<ICollection<PostDTO>>>;
