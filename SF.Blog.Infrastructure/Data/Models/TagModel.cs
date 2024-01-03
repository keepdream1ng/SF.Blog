﻿namespace SF.Blog.Infrastructure.Data.Models;
public class TagModel : IDbModel
{
	public int Id { get; set; }
	public string Value { get; set; }
	public IEnumerable<PostModel> Posts { get; set; }
}
