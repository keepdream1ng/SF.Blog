using MediatR;
using Microsoft.AspNetCore.Mvc;
using SF.Blog.Infrastructure.Mediator.Queries;
using SF.Blog.Infrastructure.Data.DTO;
using Ardalis.Result;
using SF.Blog.Web.Views.Tags;

namespace SF.Blog.Web.Controllers;

public class TagsController(IMediator Mediator) : Controller
{
	[HttpGet]
	public async Task<IActionResult> GetAll()
	{
        Result<ICollection<TagDTO>> tagsResult = await Mediator.Send(new GetAllTagsQuery());
		if (!tagsResult.IsSuccess) return NotFound();
		return View("TagCloudView", new TagCloudViewModel() { Tags = tagsResult.Value });
	}
}
