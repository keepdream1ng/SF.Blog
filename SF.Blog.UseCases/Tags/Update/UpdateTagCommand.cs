using Ardalis.Result;
using MediatR;
using SF.Blog.Core;

namespace SF.Blog.UseCases.Tags;
public record UpdateTagCommand(IUserAuth User, string PostId, string TagToUpdate, string NewTagValue) : IRequest<Result<bool>>;
