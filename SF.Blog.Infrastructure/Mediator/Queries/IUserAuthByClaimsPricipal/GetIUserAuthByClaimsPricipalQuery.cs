using Ardalis.Result;
using MediatR;
using SF.Blog.Core;
using System.Security.Claims;

namespace SF.Blog.Infrastructure.Mediator.Queries;
public record GetIUserAuthByClaimsPricipalQuery(ClaimsPrincipal User) : IRequest<Result<IUserAuth>>;
