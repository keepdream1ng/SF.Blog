using Ardalis.Result;
using MediatR;

namespace SF.Blog.Infrastructure.Mediator.Queries;
public record PostExistWithIdQuery(string Id) : IRequest<Result<bool>>;
