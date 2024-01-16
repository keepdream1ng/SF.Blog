using Ardalis.Result;
using MediatR;
using SF.Blog.Infrastructure.Data.DTO;

namespace SF.Blog.Infrastructure.Mediator.Queries;
public record GetAllRolesQuery : IRequest<Result<ICollection<RoleDTO>>>;
