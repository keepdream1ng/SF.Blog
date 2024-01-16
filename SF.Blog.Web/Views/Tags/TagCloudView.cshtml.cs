using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SF.Blog.Infrastructure.Data.DTO;

namespace SF.Blog.Web.Views.Tags;

public class TagCloudViewModel
{
	public ICollection<TagDTO> Tags { get; set; }

	public double Average {
		get
		{
			if (_average == 0)
			{
				_average = Tags.Average(t => t.Count);
			}
			return _average;
		}
	}

	private double _average = 0;
}
