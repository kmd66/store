using MizeBazi.Store.Common.Abstractions;
using MizeBazi.Store.Common.Helper;
using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Api.Helper;

public class RequestInfo : IRequestInfo
{

    private readonly IHttpContextAccessor _httpContextAccessor;

    public RequestInfo(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        if (_httpContextAccessor.HttpContext?.Request != null && _httpContextAccessor.HttpContext.Request.Headers.ContainsKey("Auth"))
        {
            var token = _httpContextAccessor.HttpContext.Request.Headers["Auth"];
            var model = new JwtHelper().Decode(token);
            UserId = model.UserId;
            Role = model.Role;
        }
    }

    private RequestInfo()
    {
    }

    public long UserId { get; set; }

    public UserRoles Role { get; set; }

}