﻿namespace SF.Blog.Core;
public interface IRepository<T> where T : class, IDomainEntity
{
	Task<T?> GetByIdAsync(string Id);
	Task<T> AddAsync(T entity);
	Task UpdateAsync(T entity);
	Task DeleteAsync(T entity);
}
