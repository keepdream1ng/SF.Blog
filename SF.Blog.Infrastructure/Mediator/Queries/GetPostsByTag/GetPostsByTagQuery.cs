using Ardalis.Result;
using MediatR;
using SF.Blog.Infrastructure.Data.DTO;

namespace SF.Blog.Infrastructure.Mediator.Queries;
public record GetPostsByTagQuery(string Tag) : IRequest<Result<ICollection<PostDTO>>>;
