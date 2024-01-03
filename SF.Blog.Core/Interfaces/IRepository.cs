namespace SF.Blog.Core;
public interface IRepository<T> where T : class, IDomainEntity
{
	Task<T> GetByIdAsync(string Id);
	Task<List<T>> ListAsync();
	Task<T> AddAsync(T entity);
	Task<T> UpdateAsync(T entity);
	Task DeleteAsync(T entity);
}
