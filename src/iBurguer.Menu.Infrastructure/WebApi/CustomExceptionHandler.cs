using System.Diagnostics;
using static iBurguer.Menu.Core.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace iBurguer.Menu.Infrastructure.WebApi;

public sealed class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError("Error Message: {exceptionMessage}, Occurred at: {time}",
            exception.Message, DateTime.UtcNow);

        int statusCode = GetStatusCodeFromException(exception);

        ProblemDetails problemDetails = new()
        {
            Type = "https://httpstatuses.com/" + statusCode,
            Title = exception.GetType().Name,
            Detail = exception.Message,
            Status = statusCode,
            Instance = httpContext.Request.Path
        };
        
        problemDetails.Extensions.Add(new KeyValuePair<string, object?>("traceId", Activity.Current?.Id ?? httpContext.TraceIdentifier));
        
        httpContext.Response.StatusCode = statusCode;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }

    private int GetStatusCodeFromException(Exception exception) => exception switch
    {
        MenuItemNotFound => StatusCodes.Status404NotFound,
        InvalidPrice => StatusCodes.Status422UnprocessableEntity,
        InvalidCategory => StatusCodes.Status422UnprocessableEntity,
        InvalidUrl => StatusCodes.Status422UnprocessableEntity,
        InvalidTime => StatusCodes.Status422UnprocessableEntity,
        MaxTime => StatusCodes.Status422UnprocessableEntity,

        _ => StatusCodes.Status500InternalServerError
    };
}