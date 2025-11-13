
using MizeBazi.Store.Api.Middleware;

namespace MizeBazi.Store.Api.Helper;
public static class MiddlewareExtensions
{
    public static IApplicationBuilder SetAppMiddlewares(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestInfoMiddleware>();
    }
}
