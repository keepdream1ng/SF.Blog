using Ardalis.Result;
using MediatR;
using Microsoft.Extensions.Logging;

namespace SF.Blog.Infrastructure.Mediator.Behaviors;
public class LoggingPipelineBehavior<TRequest, TResponse>(
    ILogger<LoggingPipelineBehavior<TRequest, TResponse>> logger
    ): IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IResult
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        // Logging best practices, right?
        if (logger.IsEnabled(LogLevel.Information))
        {
            logger.LogInformation(
                "Start of request {@RequestName}, {@DateTimeUtc}",
                typeof(TRequest).Name,
                DateTime.UtcNow);
        }

        var result = await next();

        if (logger.IsEnabled(LogLevel.Information))
        {
            logger.LogInformation(
                "Completed request {@RequestName} {@Status}, {@DateTimeUtc}",
                typeof(TRequest).Name,
                result.Status,
                DateTime.UtcNow);
        }

        // Error logging.
        if (result.Status == ResultStatus.Error)
        {
            logger.LogError(
                "Error on request {@RequestName}, {@Errors}, {@DateTimeUtc}",
                typeof(TRequest).Name,
                result.Errors,
                DateTime.UtcNow);
        }

        return result;
    }
}
