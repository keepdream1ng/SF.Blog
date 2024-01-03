using AutoMapper;
using SF.Blog.Core;
using SF.Blog.Infrastructure.Data.Models;

namespace SF.Blog.Infrastructure.Mapping;
public class MappingProfile : Profile
{
	public MappingProfile()
	{
		ShouldMapField = fieldInfo => true;
		ShouldMapProperty = propertyInfo => true;
		// From db model to domain entity.
		CreateMap<CommentModel, Comment>();
		CreateMap<TagPost, Tag>();
		CreateMap<PostModel, Post>();
		CreateMap<PostModelToPostMapperHelper, Post>();
		CreateMap<AppUserModel, User>();
		CreateMap<UserModelToUserMapperHelper, User>();

		// From domain entity to db model.
		CreateMap<Comment, CommentModel>();
		CreateMap<Tag, TagModel>();
		CreateMap<User, AppUserModel>();
		CreateMap<Post, PostModel>()
			.ForMember(p => p.Tags,
			opt => opt.Ignore()
			);
	}
}
