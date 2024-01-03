﻿namespace SF.Blog.Core;

/// <summary>
/// This interface is for facade pattern to separate domain aggregates with framework based entities for authorization and authentication.
/// </summary>
public interface IPostRepository : IRepository<Post>
{
	Task<Post> AddTagAsync(string postId, string tag);
	Task<Post> RemoveTagAsync(string postId, Tag tag);
}
