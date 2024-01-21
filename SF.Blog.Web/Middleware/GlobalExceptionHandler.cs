using Microsoft.AspNetCore.Diagnostics;
using SF.Blog.Core;

namespace SF.Blog.Web.Middleware;

public class GlobalExceptionHandler(
    ILogger<GlobalExceptionHandler> logger,
    IHttpContextAccessor httpContextAccessor
    ) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError(
            "Error on request {@RequestPath}, {@Error}, {@DateTimeUtc}",
            httpContext.Request.Path.Value,
            exception.ToString(),
            DateTime.UtcNow);

        // Check if the request is for an API route
        bool isApiRequest = httpContext.Request.Path.StartsWithSegments("/api");

        if ( isApiRequest )
        {
            httpContext.Response.StatusCode = 500;
            httpContext.Response.ContentType = "application/json";
            await httpContext.Response.WriteAsJsonAsync(exception, cancellationToken);
        }
        else
        {
            httpContextAccessor.HttpContext.Response.Redirect("/Home/Oops?code=500");
        }

        return true;
    }
}
