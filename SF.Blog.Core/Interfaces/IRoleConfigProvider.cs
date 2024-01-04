namespace SF.Blog.Core;
public interface IRoleConfigProvider
{
	Role[] GetAdminRoles();
	Role[] GetModeratorRoles();
}
