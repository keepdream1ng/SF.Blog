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
		CreateMap<TagModel, Tag>();
		CreateMap<PostModel, Post>();
		CreateMap<AppUserModel, User>();

		// From domain entity to db model.
		CreateMap<Comment, CommentModel>();
		CreateMap<Tag, TagModel>();
		CreateMap<Post, PostModel>();
		CreateMap<User, AppUserModel>();
	}
}
