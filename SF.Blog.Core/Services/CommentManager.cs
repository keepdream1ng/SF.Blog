namespace SF.Blog.Core;

/// <summary>
/// Manager class, what exposes to public internal <see cref="Comment"/> aggregate methods. Can be get after domain level access check via <see cref="AuthForManagerService"/>.
/// </summary>
public class CommentManager
{
	public Comment ManagedComment { get; private set; }
	private readonly IRepository<Comment> _commentRepo;

	// Constructor is internal so only domain services can create manager after checking user access.
    internal CommentManager(Comment comment, IRepository<Comment> commentRepo)
    {
		ManagedComment = comment;
		_commentRepo = commentRepo;
	}

	public async Task<Comment> UpdateCommentAsync(string newText)
	{
		ManagedComment.Update(newText);
		await _commentRepo.UpdateAsync(ManagedComment);
		return ManagedComment;
	}

	public async Task<bool> DeleteAsync()
	{
		await _commentRepo.DeleteAsync(ManagedComment);
		return true;
	}
}
