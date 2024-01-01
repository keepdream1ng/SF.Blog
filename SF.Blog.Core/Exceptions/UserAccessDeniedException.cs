using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF.Blog.Core;
public class UserAccessDeniedException : Exception
{
	public override string Message { get; } = "User ownership or role doesnt support requested access";
}
