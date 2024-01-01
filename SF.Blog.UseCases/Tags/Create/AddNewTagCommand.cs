using Ardalis.Result;
using MediatR;
using SF.Blog.Core;

namespace SF.Blog.UseCases.Tags;
public record AddNewTagCommand(IUserAuth User, string PostId, string Tag) : IRequest<Result<bool>>;
