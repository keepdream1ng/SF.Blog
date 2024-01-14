using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF.Blog.Infrastructure.Utility;
public static class StringExtensions
{
	public static string GetFirstNCharacters(this string input, int n)
	{
		return input.Substring(0, Math.Min(n, input.Length));
	}

	public static string TrimToLastWholeWord(this string input)
	{
		int lastSpaceIndex = input.LastIndexOf(' ');

		if (lastSpaceIndex != -1)
		{
			return input.Substring(0, lastSpaceIndex);
		}

		// If there are no spaces, return the original string
		return input;
	}
}
