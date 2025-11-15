using Microsoft.AspNetCore.Mvc.Filters;
using MizeBazi.Store.Common.Abstractions;
using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Api.Middleware;

//[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
//public class AuthFilter(IRequestInfo requestInfo) : Attribute, IAuthorizationFilter
//{

//    public void OnAuthorization(AuthorizationFilterContext context)
//    {
//        var authAttribute = context.ActionDescriptor.EndpointMetadata
//            .OfType<AuthAttribute>()
//            .FirstOrDefault();

//        var requestInfo1 = context.HttpContext.RequestServices.GetService<IRequestInfo>();
//        var requestInfo2 = context.HttpContext.RequestServices.GetService<IRequestInfo>();

//        // برای Scoped باید یکسان باشند در یک درخواست
//        Console.WriteLine($"Same instance in same request: {requestInfo1 == requestInfo2}");
//        var requiredRole = authAttribute?.Role ?? UserRoles.Guest;
        

//        if (requestInfo.Role < requiredRole)
//        {
//            //context.Result = new UnauthorizedResult();
//            //return;
//        }
//    }

//}
