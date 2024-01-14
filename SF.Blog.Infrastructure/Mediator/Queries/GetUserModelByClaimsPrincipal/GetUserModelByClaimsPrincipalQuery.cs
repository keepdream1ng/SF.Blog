using Ardalis.Result;
using MediatR;
using SF.Blog.Infrastructure.Data.Models;
using System.Security.Claims;

namespace SF.Blog.Infrastructure.Mediator.Queries;
public record GetUserModelByClaimsPrincipalQuery(ClaimsPrincipal User) : IRequest<Result<AppUserModel>>;
