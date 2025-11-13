using MizeBazi.Store.Common.Shared;
using Microsoft.AspNetCore.Mvc;

namespace MizeBazi.Store.Api.Middleware;

public class AuthAttribute : TypeFilterAttribute
{
    public UserRoles Role { get; }
    public AuthAttribute() : base(typeof(AuthFilter))
    {
        Role = UserRoles.Guest;
    }

    public AuthAttribute(UserRoles role) : base(typeof(AuthFilter))
    {
        Role = role;
    }
}
