
using MizeBazi.Store.Api.Middleware;

namespace MizeBazi.Store.Api.Helper;
public static class MiddlewareRegistration
{
    public static IApplicationBuilder SetAppMiddlewares(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<ExceptionMiddleware>();

        builder.UseMiddleware<RequestInfoMiddleware>();

        return builder;
    }
}
