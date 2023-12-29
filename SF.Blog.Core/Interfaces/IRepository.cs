using Ardalis.Specification;

namespace SF.Blog.Core;
public interface IRepository<T> : IRepositoryBase<T> where T : class, IDomainEntity
{
}
