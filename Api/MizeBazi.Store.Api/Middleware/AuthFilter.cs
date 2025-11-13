using Microsoft.AspNetCore.Mvc.Filters;
using MizeBazi.Store.Common.Abstractions;
using MizeBazi.Store.Common.Shared;
using Microsoft.AspNetCore.Mvc;

namespace MizeBazi.Store.Api.Middleware;

[AttributeUsage(AttributeTargets.Method)]
public class AuthFilter(IRequestInfo requestInfo) : Attribute, IAuthorizationFilter
{
    private readonly IRequestInfo _requestInfo = requestInfo;

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var authAttribute = context.ActionDescriptor.EndpointMetadata
            .OfType<AuthAttribute>()
            .FirstOrDefault();

        var requiredRole = authAttribute?.Role ?? UserRoles.Guest;

        if (_requestInfo.Role < requiredRole)
        {
            context.Result = new UnauthorizedResult();
            return;
        }
    }

}
