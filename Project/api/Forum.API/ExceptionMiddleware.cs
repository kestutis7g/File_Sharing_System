using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;
using System.Reflection;

namespace Forum;

public class ErrorObject
{
    public int StatusCode { get; set; }
    public string? ErrorCode { get; set; }
    public string? Message { get; set; }
}
public class ExceptionMiddleware
{
    public ExceptionMiddleware(RequestDelegate next)
    {
        Next = next;
    }

    private readonly RequestDelegate Next;

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await Next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    public async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        ForumExceptionAttribute? attribute = exception.GetType().GetCustomAttribute<ForumExceptionAttribute>();

        context.Response.ContentType = "application/json";

        var contextResponse = context.Response;

        contextResponse.StatusCode = (int)HttpStatusCode.InternalServerError;

        var response = new ErrorObject
        {
            StatusCode = 500,
            ErrorCode = "ERROR_UNKNOWN",
            Message = "Internal server error"
        };


        if (attribute != null)
        {
            contextResponse.StatusCode = attribute.StatusCode;
            response.StatusCode = attribute.StatusCode;
            response.ErrorCode = attribute.ErrorCode;
            response.Message = attribute.Message;
        }

        NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        logger.Error(exception);

        var result = JsonConvert.SerializeObject(response);
        await context.Response.WriteAsync(result);
    }
}   
