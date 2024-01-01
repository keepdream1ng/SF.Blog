using Ardalis.Result;
using MediatR;

namespace SF.Blog.UseCases.Tags;
public class UpdateTagHandler(IMediator Mediator) : IRequestHandler<UpdateTagCommand, Result<bool>>
{
	public async Task<Result<bool>> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
	{
		Result<bool> result = await Mediator.Send(new RemoveTagCommand(request.User, request.PostId, request.TagToUpdate));
		if (result == Result<bool>.Success(true))
		{
			result = await Mediator.Send(new AddNewTagCommand(request.User, request.PostId, request.NewTagValue));
		}
		return result;
	}
}
