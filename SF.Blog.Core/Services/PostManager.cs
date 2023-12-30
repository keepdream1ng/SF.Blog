﻿namespace SF.Blog.Core;
/// <summary>
/// Manager class, what exposes to public internal <see cref="Post"/> aggregate methods. Can be get after domain level access check via <see cref="AuthForManagerService"/>.
/// </summary>
public class PostManager
{
	public Post ManagedPost { get; private set; }
	private readonly IRepository<Post> _postRepo;

	// Constructor is internal so only domain services can create manager after checking user access.
	internal PostManager(Post post, IRepository<Post> postRepo)
    {
		ManagedPost = Guard.Against.Null(post);
		_postRepo = Guard.Against.Null(postRepo);
	}

	public async Task<Post> UpdatePostAsync(string title, string content)
	{
		ManagedPost.Update(title, content);
		await _postRepo.UpdateAsync(ManagedPost);
		return ManagedPost;
	}

	public async Task<Post> AddTagAsync(string tag)
	{
		ManagedPost.AddTag(tag);
		await _postRepo.UpdateAsync(ManagedPost);
		return ManagedPost;
	}

	public async Task<Post> RemoveTagAsync(Tag tag)
	{
		ManagedPost.RemoveTag(tag);
		await _postRepo.UpdateAsync(ManagedPost);
		return ManagedPost;
	}
	public async Task<bool> DeleteAsync()
	{
		await _postRepo.DeleteAsync(ManagedPost);
		return true;
	}
}