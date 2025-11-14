using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using MizeBazi.Store.Common.Abstractions;
using MizeBazi.Store.Common.Helper;
using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Api.Middleware;

public class RequestInfoMiddleware
{
    private readonly RequestDelegate _next;

    public RequestInfoMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ICacheService cacheService, IRequestInfo requestInfo)
    {
        if (AppSetings.IsDevelopment)
            requestInfo = RequestInfo.GetInstance(20, UserRoles.Admin);


        string tokenId = context.Request.Headers["tokenId"];
        string userId = context.Request.Headers["userId"];

        if (!userId.IsNullOrEmpty() && !tokenId.IsNullOrEmpty())
        {
            cacheService.TryGetValue<CacheInMemoryRecord>("token", userId, out var lastCallTime);
            if (lastCallTime != null && tokenId == lastCallTime.TokenId.ToString())
                requestInfo = RequestInfo.GetInstance(lastCallTime.UserId, lastCallTime.Role);
            else
                requestInfo = RequestInfo.GetInstance(0, UserRoles.Guest);
        }

        await _next(context);
    }
}

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class PopulateRequestInfoAttribute : Attribute, IActionFilter
{

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var requestInfo = context.HttpContext.RequestServices.GetRequiredService<IRequestInfo>();
        if (AppSetings.IsDevelopment)
            requestInfo = RequestInfo.GetInstance(20, UserRoles.Admin);

        string tokenId = context.HttpContext.Request.Headers["tokenId"];
        string userId = context.HttpContext.Request.Headers["userId"];
        if (!userId.IsNullOrEmpty() && !tokenId.IsNullOrEmpty())
        {
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();
            cacheService.TryGetValue<CacheInMemoryRecord>("token", userId, out var lastCallTime);
            if (lastCallTime != null)
                requestInfo = RequestInfo.GetInstance(lastCallTime.UserId, lastCallTime.Role);
            else
                requestInfo = RequestInfo.GetInstance(0, UserRoles.Guest);
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}


