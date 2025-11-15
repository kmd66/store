using MizeBazi.Store.Common.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MizeBazi.Store.Common.Abstractions;
using Microsoft.AspNetCore.Authorization;

namespace MizeBazi.Store.Api.Middleware;

public class AuthAttribute : Attribute, IAuthorizationFilter
{
    public UserRoles Role { get; }
    public AuthAttribute() 
    {
        Role = UserRoles.Admin;
    }

    public AuthAttribute(UserRoles role)
    {
        Role = role;
    }
    public virtual void OnAuthorization(AuthorizationFilterContext context)
    {
        if (HasAllowAnonymous(context))
        {
            return; 
        }
        var requestInfo = context.HttpContext.RequestServices.GetService<IRequestInfo>();
        if (requestInfo.Role < Role)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

    }
    private bool HasAllowAnonymous(AuthorizationFilterContext context)
    {
        return context.ActionDescriptor.EndpointMetadata
            .Any(x => x is AllowAnonymousAttribute);
    }
}
