using Microsoft.AspNetCore.Mvc.Filters;

namespace MizeBazi.Store.Api.Middleware;

public class AuthForControllerBaseAttribute : AuthAttribute
{
    public AuthForControllerBaseAttribute() : base()
    {
    }
    public override void OnAuthorization(AuthorizationFilterContext context)
    {
        var actionHasExplicitAuth = context.ActionDescriptor.EndpointMetadata
            .OfType<AuthAttribute>()
            .Any(a => a.GetType() == typeof(AuthAttribute));

        if (actionHasExplicitAuth)
            return;
        base.OnAuthorization(context);
    }
}
