using Ardalis.Result;
using MediatR;
using SF.Blog.Infrastructure.Data.Models;

namespace SF.Blog.Infrastructure.Mediator.Queries;
public record GetPostModelByIdQuery(string Id) : IRequest<Result<PostModel>>;
