﻿namespace SF.Blog.Core;
/// <summary>
/// This interface is for facade pattern to separate domain aggregates with framework based entities for authorization and authentication.
/// </summary>
public interface IUserRepository : IRepository<User>
{
	Task<User> AddToRoleAsync(User user, string role);
	Task<User> RemoveFromRoleAsync(User user, Role role);
}
