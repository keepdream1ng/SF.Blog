﻿namespace SF.Blog.Core;

public interface IPost
{
	string Content { get; }
	string Id { get; }
	DateTime? Modified { get; }
	string OwnerId { get; }
	DateTime Published { get; }
	string Title { get; }
}