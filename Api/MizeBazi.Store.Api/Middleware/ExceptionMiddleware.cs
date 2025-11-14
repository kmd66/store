using MizeBazi.Store.Common.Abstractions;
using MizeBazi.Store.Common.Helper;
using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Api.Middleware;


public class ExceptionMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var response = context.Response;
        response.ContentType = "application/json";

        var errorResponse = AppException.Response(exception.Message);

        switch (exception)
        {
            case AppTokenException:
            case UnauthorizedAccessException:
                errorResponse = AppTokenException.Response();
                break;

            case AppNotFoundException:
            case KeyNotFoundException:
                errorResponse = AppNotFoundException.Response();
                break;

            case ArgumentException:
            case InvalidOperationException:
                errorResponse = AppException.Response(exception.Message,code:400); 
                break;

            case AppTimeoutException:
            case TimeoutException:
                errorResponse = AppTimeoutException.Response(); 
                break;

            default:
                errorResponse = AppException.Response(exception.Message, code: 500);
                break;
        }

        if (errorResponse.code != -1)
        {
            var logger = context.RequestServices.GetRequiredService<IAppLogger<AppException>>();
            if(errorResponse.code == 500 || errorResponse.code == 408)
                logger.LogError($"Exception code {errorResponse.code}, message: {errorResponse.message}");
            else
                logger.LogWarning($"Exception code {errorResponse.code}, message: {errorResponse.message}");
        }

        response.StatusCode = StatusCodes.Status200OK;
        if (errorResponse.code == 500 || errorResponse.code == 408)
            response.StatusCode = errorResponse.code;

        var jsonResponse = errorResponse.ToJson();
        await response.WriteAsync(jsonResponse);
    }
}