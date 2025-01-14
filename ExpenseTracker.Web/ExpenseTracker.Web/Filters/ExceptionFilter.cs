using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace ExpenseTracker.Web.Filters;

public class ExceptionFilter : IExceptionFilter
{
    private readonly ILogger<ExceptionFilter> _logger;

    public ExceptionFilter(ILogger<ExceptionFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        _logger.LogError(context.Exception, "Unhandled error occurred, {Message}", context.Exception.Message);

        var statusCode = context.Exception switch
        {
            HttpRequestException exception => GetStatusCode(exception.StatusCode),
            UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
            _ => (int)HttpStatusCode.InternalServerError,
        };

        context.Result = new RedirectToActionResult("Error", "Home", new { statusCode });
        context.ExceptionHandled = true;
    }

    private static int GetStatusCode(HttpStatusCode? statusCode) =>
        statusCode is null
        ? (int)HttpStatusCode.InternalServerError
        : (int)statusCode;
}
