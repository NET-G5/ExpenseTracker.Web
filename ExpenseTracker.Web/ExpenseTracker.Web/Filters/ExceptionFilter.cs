using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace ExpenseTracker.Web.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
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
