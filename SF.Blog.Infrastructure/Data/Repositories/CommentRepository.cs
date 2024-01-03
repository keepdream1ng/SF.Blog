using Ardalis.Specification;
using AutoMapper;
using SF.Blog.Core;
using SF.Blog.Infrastructure.Data.Models;

namespace SF.Blog.Infrastructure.Data.Repositories;
public class CommentRepository : IRepository<Comment>
{
	private readonly IRepositoryBase<CommentModel> _modelRepo;
	private readonly IMapper _mapper;

	public CommentRepository(
		IRepositoryBase<CommentModel> modelRepo,
		IMapper mapper
		)
	{
		_modelRepo = modelRepo;
		_mapper = mapper;
	}
	public async Task<Comment> AddAsync(Comment entity)
	{
		var dbModel = _mapper.Map<CommentModel>(entity);
		var result = await _modelRepo.AddAsync(dbModel);
		return _mapper.Map<Comment>(result);
	}

	public async Task DeleteAsync(Comment entity)
	{
		var dbModel = await _modelRepo.GetByIdAsync(entity.Id);
		if (dbModel is not null)
			await _modelRepo.DeleteAsync(dbModel);
	}

	public async Task<Comment?> GetByIdAsync(string Id)
	{
		var dbModel = await _modelRepo.GetByIdAsync(Id);
		if (dbModel is null) return null;
		return _mapper.Map<Comment>(dbModel);
	}

	public async Task UpdateAsync(Comment entity)
	{
		var dbModel = _mapper.Map<CommentModel>(entity);
		await _modelRepo.UpdateAsync(dbModel);
	}
}
