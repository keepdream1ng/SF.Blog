using Ardalis.Specification;
using AutoMapper;
using SF.Blog.Core;
using SF.Blog.Infrastructure.Data.Models;
using SF.Blog.Infrastructure.Data.Models.Specifications;

namespace SF.Blog.Infrastructure.Data.Repositories;

/// <summary>
/// Facade repository for domain post entities.
/// </summary>
public class PostRepository : IPostRepository
{
	private readonly IRepositoryBase<PostModel> _postModelRepo;
	private readonly IRepositoryBase<TagModel> _tagModelRepo;
	private readonly IRepositoryBase<TagPost> _tagPostRepo;
	private readonly IMapper _mapper;

	public PostRepository(
		IRepositoryBase<PostModel> postModelRepo,
		IRepositoryBase<TagModel> tagModelRepo,
		IRepositoryBase<TagPost> tagPostRepo,
		IMapper mapper
		)
    {
		_postModelRepo = postModelRepo;
		_tagModelRepo = tagModelRepo;
		_tagPostRepo = tagPostRepo;
		_mapper = mapper;
	}
	public async Task<Post?> GetByIdAsync(string Id)
	{
		var postModel = await _postModelRepo.SingleOrDefaultAsync(new PostModelByIdSpec(Id));
		return _mapper.Map<Post?>(postModel);
	}

    public async Task<Post> AddAsync(Post entity)
	{
		var dbModel = _mapper.Map<PostModel>(entity);
		var result = await _postModelRepo.AddAsync(dbModel);
		return _mapper.Map<Post>(result);
	}

	public async Task AddTagAsync(string postId, string tag)
	{
		var tagModel = await _tagModelRepo.SingleOrDefaultAsync(new TagByValueSpec(tag));
		// If tag doesnt exist in the db.
		if (tagModel == null)
		{
			tagModel = await _tagModelRepo.AddAsync(new TagModel() { Value = tag });
		}
		// Creating new relationship object in the junction table.
		await _tagPostRepo.AddAsync(new TagPost() { PostId = postId, TagId = tagModel.Id });
	}
	public async Task RemoveTagAsync(string postId, Tag tag)
	{
		var tagPost = await _tagPostRepo.SingleOrDefaultAsync(new TagPostByPostIdAndTagSpec(postId, tag.Value));
		if (tagPost is not null)
			await _tagPostRepo.DeleteAsync(tagPost);
	}

	public async Task UpdateAsync(Post entity)
	{
		var dbModel = _mapper.Map<PostModel>(entity);
		await _postModelRepo.UpdateAsync(dbModel);
	}

	public async Task DeleteAsync(Post entity)
	{
		var dbModel = await _postModelRepo.GetByIdAsync(entity.Id);
		if (dbModel is not null)
			await _postModelRepo.DeleteAsync(dbModel);
	}
}
