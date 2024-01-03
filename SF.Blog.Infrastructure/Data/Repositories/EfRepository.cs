using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;

namespace SF.Blog.Infrastructure.Data.Repositories;

/// <summary>
/// Generic repository for entity framework DBsets, supports specifications.
/// </summary>
public class EfRepository<T> : RepositoryBase<T>, IRepositoryBase<T> where T : class, IDbModel
{
	public EfRepository(ApplicationDbContext dbContext) : base(dbContext)
	{
	}
}
