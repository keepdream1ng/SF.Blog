using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SF.Blog.Infrastructure.Data.Models;
using SF.Blog.UseCases.Users;
using SF.Blog.Core;
using AutoMapper;

namespace SF.Blog.Infrastructure.Mediator.Commands;
public class RegisterUserHandler(
	UserManager<AppUserModel> UserManager,
	IMapper Mapper,
	IMediator Mediator
	) : IRequestHandler<RegisterUserCommand, Result<User>>
{
	public async Task<Result<User>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
	{
		// Check if email already registered.
		var registeredUser = await UserManager.FindByEmailAsync( request.Email );
		if (registeredUser is not null) return Result.Conflict();

		// Upplying Domain constraints to user creation.
		Result<User> userResult = await Mediator.Send(new CreateUserCommand(request.Name, request.About, request.DateOfBirth));
		if (!userResult.IsSuccess) return Result.Invalid(userResult.ValidationErrors);

		// Doing framework level registration.
		AppUserModel userModel = Mapper.Map<User, AppUserModel>(userResult.Value);
		userModel.Email = request.Email;
		await UserManager.CreateAsync(userModel, request.Password);
		await UserManager.AddToRoleAsync(userModel, "User");

		return await Mediator.Send(new GetUserByIdQuery(userModel.Id));
	}
}
