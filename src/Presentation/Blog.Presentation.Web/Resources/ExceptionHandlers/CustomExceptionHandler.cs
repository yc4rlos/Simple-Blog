using System.Text.Json;
using Blog.Presentation.Web.Resources.Responses;
using Microsoft.AspNetCore.Diagnostics;

namespace Blog.Presentation.Web.Resources.ExceptionHandlers;

public class CustomExceptionHandler: IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var statusCode = exception switch
        {
            BadRequestException => StatusCodes.Status400BadRequest,
            NotFoundException => StatusCodes.Status404NotFound,
            UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
            _ => StatusCodes.Status500InternalServerError
        };
        
        var response = new DefaultResponse
        {
            Message = exception.Message
        };

        if (exception is BadRequestException badRequestException)
        {
            response.Errors = badRequestException.Failures;
        }
        
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = statusCode;
        
        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response), cancellationToken);
        return true;
    }
}